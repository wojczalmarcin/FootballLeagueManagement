using Application.DTO;
using Application.Interfaces.Validators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.Season
{
    /// <summary>
    /// Validates season data transfer object
    /// </summary>
    public class SeasonValidator : Validator, ISeasonValidator
    {

        private readonly ISeasonRepository _seasonRepository;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="seasonRepository">The season repository</param>
        public SeasonValidator(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }


        /// <summary>
        /// Validates season existence
        /// </summary>
        /// <param name="season">season</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateSeasonExistence(SeasonDto season)
            => ValidateEntityExistence(season, "Season with given Id doesn't exist");

        /// <summary>
        /// Validates season editing
        /// </summary>
        /// <param name="season">The season</param>
        /// <param name="seasonToEdit">The season to edit</param>
        /// <returns>Validation result</returns>
        public async Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateSeasonEditAsync(SeasonDto season, SeasonDto seasonToEdit)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validation = this.ValidateSeasonExistence(seasonToEdit);
            var datesValidation = await DatesValidationAsync(season);

            if(datesValidation.statusCode != HttpStatusCode.OK)
            {
                validation.validationErrors.AddRange(datesValidation.validationErrors);
                validation.statusCode = datesValidation.statusCode;
            }

            return validation;
        }

        /// <summary>
        /// Validates season creation
        /// </summary>
        /// <param name="season">Season to add</param>
        /// <returns>Validation result</returns>
        public async Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateSeasonCreation(CreateSeasonDto season)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validation = (HttpStatusCode.OK, new List<string>());

            validation = await DatesValidationAsync(season);

            return validation;
        }

        private async Task<(HttpStatusCode statusCode, List<string> validationErrors)> DatesValidationAsync(CreateSeasonDto seasonDto)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validationResult = 
                (HttpStatusCode.OK, new List<string>());

            var seasons = (await _seasonRepository.GetAllSeasonsAsync()).ToList();

            var type = seasonDto.GetType();
            foreach (var season in seasons)
            {
                if (typeof(SeasonDto) == seasonDto.GetType())
                {
                    var editSeason = (SeasonDto)seasonDto;
                    if (editSeason.Id != season.Id)
                    {
                        if (seasonDto.StartDate.Year == season.StartDate.Year)
                            validationResult = (HttpStatusCode.BadRequest, new List<string>() { $"Season starting in {seasonDto.StartDate.Year} year already exists!" });

                        if (seasonDto.StartDate < season.EndDate && seasonDto.EndDate> season.StartDate)
                        {
                            validationResult.statusCode = HttpStatusCode.BadRequest;
                            validationResult.validationErrors.Add($"Starting date of season {seasonDto.StartDate.Year} cannot be earlier than ending date of season {season.StartDate.Year}");
                        }
                    }
                }
                else if(seasonDto.StartDate.Year == season.StartDate.Year)
                {
                   validationResult =(HttpStatusCode.BadRequest, new List<string>() { $"Season starting in {seasonDto.StartDate.Year} year already exists!" });
                }
            }

            if (seasonDto.StartDate >= seasonDto.EndDate)
            {
                validationResult.validationErrors.Add("Season ending date cannot be earlier than starting date");
                validationResult.statusCode = HttpStatusCode.BadRequest;
            }

            return validationResult;
        }   
    }
}
