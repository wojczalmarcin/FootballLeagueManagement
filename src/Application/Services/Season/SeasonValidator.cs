using Application.DTO;
using Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Net;

namespace Application.Services.Season
{
    /// <summary>
    /// Validates season data transfer object
    /// </summary>
    public class SeasonValidator : ISeasonValidator
    {
        /// <summary>
        /// Validates season existence
        /// </summary>
        /// <param name="season">season</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateSeasonExistence(SeasonDto season)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            if (season == null)
            {
                statusCode = HttpStatusCode.NotFound;
                validationErrors.Add("Season with given Id doesn't exist");
                return (statusCode, validationErrors);
            }

            return (statusCode, validationErrors);
        }

        /// <summary>
        /// Validates season editing
        /// </summary>
        /// <param name="season">The season</param>
        /// <param name="seasonToEdit">The season to edit</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateSeasonEdit(SeasonDto season, SeasonDto seasonToEdit)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validation = this.ValidateSeasonExistence(seasonToEdit);
            if(validation.statusCode != HttpStatusCode.OK)
            {
                return validation;
            }

            this.DatesValidation(ref validation, season);

            return validation;
        }

        /// <summary>
        /// Validates season creation
        /// </summary>
        /// <param name="season">Season to add</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateSeasonCreation(CreateSeasonDto season)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validation = (HttpStatusCode.OK, null);

            this.DatesValidation(ref validation, season);

            return validation;
        }

        private void DatesValidation(ref (HttpStatusCode statusCode, List<string> validationErrors) validationResult, CreateSeasonDto season)
        {
            var currentDate = DateTime.Now;

            if (season.StartDate < currentDate)
            {
                validationResult.validationErrors.Add("Cannot set season's starting date to past date");
                validationResult.statusCode = HttpStatusCode.BadRequest;
            }
            if (season.StartDate >= season.EndDate)
            {
                validationResult.validationErrors.Add("Season ending date cannot be earlier than starting date");
                validationResult.statusCode = HttpStatusCode.BadRequest;
            }
        }
    }
}
