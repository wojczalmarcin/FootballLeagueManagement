using Application.DTO;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Season
{
    /// <summary>
    /// The season service
    /// </summary>
    public class SeasonService : Service, ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;

        private readonly ISeasonValidator _seasonValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seasonRepository">The season repository</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="seasonValidator">The season validator</param>
        public SeasonService(ISeasonRepository seasonRepository, IMapper mapper, ISeasonValidator seasonValidator) : base(mapper)
        {
            _seasonRepository = seasonRepository;
            _seasonValidator = seasonValidator;
        }

        /// <summary>
        /// Gets season by id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data with <see cref="SeasonDto"/></returns>
        public async Task<ResponseData<SeasonDto>> GetSeasonByIdAsync(int seasonId) 
            => await GetByIdAsync<SeasonDto, Domain.Entities.Season>(seasonId, 
                _seasonRepository.GetSeasonByIdAsync, 
                _seasonValidator.ValidateSeasonExistence); 
        

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Response data with collection of <see cref="SeasonDto"/></returns>
        public async Task<ResponseData<IEnumerable<SeasonDto>>> GetAllSeasonsAsync()
        {
            var responseData = await GetAllAsync<SeasonDto, Domain.Entities.Season>(_seasonRepository.GetAllSeasonsAsync,
                 _seasonValidator.ValidateSeasonExistence);

            responseData.Data = responseData.Data.ToList().OrderByDescending(s=>s.EndDate);
            return responseData;
        }
            

        /// <summary>
        /// Edits season
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Response data witch edited season</returns>
        public async Task<ResponseData<SeasonDto>> EditSeasonAsync(SeasonDto season)
        {
            var responseData = new ResponseData<SeasonDto>();

            var seasonToEdit = _mapper.Map<SeasonDto>(await _seasonRepository.GetSeasonByIdAsync(season.Id));

            var editingValidation = await _seasonValidator.ValidateSeasonEditAsync(season, seasonToEdit);

            responseData.ResponseStatus = editingValidation.statusCode;
            responseData.ValidationErrors = editingValidation.validationErrors;

            if (editingValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }
            
            var editSeason = await _seasonRepository.EditSeasonAsync(_mapper.Map<Domain.Entities.Season>(season));
            if(editSeason)
            {
                responseData.Data = season;
            }
            else
            {
                responseData.ValidationErrors.Add("There was a problem with editing season");
            }

            return responseData;
        }

        /// <summary>
        /// Creates new season
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Response data with created season</returns>
        public async Task<ResponseData<SeasonDto>> CreateSeasonAsync(CreateSeasonDto season)
        {
            var responseData = new ResponseData<SeasonDto>();

            var creationValidation = await _seasonValidator.ValidateSeasonCreation(season);

            responseData.ResponseStatus = creationValidation.statusCode;
            responseData.ValidationErrors = creationValidation.validationErrors;

            if (creationValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            var seasonId = await _seasonRepository.AddSeasonAsync(_mapper.Map<Domain.Entities.Season>(season));
            
            if(seasonId > 0)
            {
                var addedSeason = _mapper.Map<SeasonDto>(season);
                addedSeason.Id = seasonId;
                responseData.Data = addedSeason;
            }
            else
            {
                responseData.ValidationErrors.Add("There was a problem with creating season");
            }

            return responseData;
        }
    }
}
