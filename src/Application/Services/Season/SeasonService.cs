using Application.DTO;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.Season
{
    /// <summary>
    /// The season service
    /// </summary>
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;

        private readonly IMapper _mapper;

        private readonly ISeasonValidator _seasonValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seasonRepository">The season repository</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="seasonValidator">The season validator</param>
        public SeasonService(ISeasonRepository seasonRepository, IMapper mapper, ISeasonValidator seasonValidator)
        {
            _seasonRepository = seasonRepository;
            _mapper = mapper;
            _seasonValidator = seasonValidator;
        }

        /// <summary>
        /// Gets season by id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data with <see cref="SeasonDto"/></returns>
        public async Task<ResponseData<SeasonDto>> GetSeasonById(int seasonId)
        {
            var responseData = new ResponseData<SeasonDto>();

            var season = _mapper.Map<SeasonDto>(await _seasonRepository.GetSeasonById(seasonId));
            var seasonValidation = _seasonValidator.ValidateSeasonExistence(season);

            responseData.ResponseStatus = seasonValidation.statusCode;
            responseData.ValidationErrors = seasonValidation.validationErrors;

            if (seasonValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            responseData.Data = season;
            return responseData;
        }

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Response data with collection of <see cref="SeasonDto"/></returns>
        public async Task<ResponseData<IEnumerable<SeasonDto>>> GetAllSeasons()
        {
            var responseData = new ResponseData<IEnumerable<SeasonDto>>();

            var seasons = _mapper.Map<IEnumerable<SeasonDto>>(await _seasonRepository.GetAllSeasons());
            var seasonValidation = _seasonValidator.ValidateSeasonExistence(seasons.ToList()[0]);

            responseData.ResponseStatus = seasonValidation.statusCode;
            responseData.ValidationErrors = seasonValidation.validationErrors;

            if (seasonValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            responseData.Data = seasons;
            return responseData;
        }
    }
}
