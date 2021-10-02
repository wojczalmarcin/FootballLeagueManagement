using Application.DTO;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.Team
{
    /// <summary>
    /// The team service
    /// </summary>
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        private readonly ISeasonRepository _seasonRepository;

        private readonly IMapper _mapper;

        private readonly ITeamValidator _teamValidator;

        private readonly ISeasonValidator _seasonValidator;

        /// <summary>
        /// The controller
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="seasonRepository">The season repository</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="teamValidator">The team validator</param>
        /// <param name="seasonValidator">The season validator</param>
        public TeamService(
            ITeamRepository teamRepository, 
            ISeasonRepository seasonRepository,
            IMapper mapper,
            ITeamValidator teamValidator,
            ISeasonValidator seasonValidator)
        {
            _teamRepository = teamRepository;
            _seasonRepository = seasonRepository;
            _mapper = mapper;
            _teamValidator = teamValidator;
            _seasonValidator = seasonValidator;
        }

        /// <summary>
        /// Gets team by id
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <returns>The response data</returns>
        public async Task<ResponseData<TeamDto>> GetTeamByIdAsync(int teamId)
        {
            var responseData = new ResponseData<TeamDto>();

            var team = _mapper.Map<TeamDto>(await _teamRepository.GetTeamByIdAsync(teamId));
            var teamValidation = _teamValidator.ValidateTeamExistence(team);

            responseData.ResponseStatus = teamValidation.statusCode;
            responseData.ValidationErrors = teamValidation.validationErrors;

            if (teamValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            responseData.Data = team;
            return responseData;
        }
        

        /// <summary>
        /// Gets teams by season Id
        /// </summary>
        /// <param name="seasonId">Seson Id</param>
        /// <returns>The response data</returns>
        public async Task<ResponseData<IEnumerable<TeamDto>>> GetTeamsBySeasonIdAsync(int seasonId)
        {
            var responseData = new ResponseData<IEnumerable<TeamDto>>();

            var season = _mapper.Map<SeasonDto>(await _seasonRepository.GetSeasonByIdAsync(seasonId));
            var seasonValidation = _seasonValidator.ValidateSeasonExistence(season);

            responseData.ResponseStatus = seasonValidation.statusCode;
            responseData.ValidationErrors = seasonValidation.validationErrors;

            if (seasonValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            responseData.Data = _mapper.Map<IEnumerable<TeamDto>>(await _teamRepository.GetTeamsBySeasonIdAsync(seasonId));

            return responseData;
        }
    }
}
