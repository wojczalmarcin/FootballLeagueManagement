using Application.DTO;
using Application.DTO.Create;
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
    public class TeamService : Service , ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        private readonly ISeasonRepository _seasonRepository;

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
            : base(mapper)
        {
            _teamRepository = teamRepository;
            _seasonRepository = seasonRepository;
            _teamValidator = teamValidator;
            _seasonValidator = seasonValidator;
        }

        /// <summary>
        /// Gets team by id
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <returns>The response data</returns>
        public async Task<ResponseData<TeamDto>> GetTeamByIdAsync(int teamId)
            => await GetByIdAsync<TeamDto, Domain.Entities.Team>(teamId, _teamRepository.GetTeamByIdAsync, _teamValidator.ValidateTeamExistence);


        /// <summary>
        /// Gets all teams
        /// </summary>
        /// <returns>The response data</returns>
        public async Task<ResponseData<IEnumerable<TeamDto>>> GetAllTeams()
            => await GetAllAsync<TeamDto, Domain.Entities.Team> (_teamRepository.GetAllTeamsAsync, _teamValidator.ValidateTeamExistence);

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

        /// <summary>
        /// Edits team
        /// </summary>
        /// <param name="team">Team with edited data</param>
        /// <returns>Response data with edited team</returns>
        public async Task<ResponseData<TeamDto>> EditTeamAsync(TeamDto team)
            => await EditAsync(team, _teamRepository.GetTeamByIdAsync, _teamRepository.EditTeamAsync, _teamValidator.ValidateTeamEdit);

        /// <summary>
        /// Creates given team
        /// </summary>
        /// <param name="team">The team to create</param>
        /// <returns>Response data with created team</returns>
        public async Task<ResponseData<TeamDto>> CreateTeamAsync(CreateTeamDto team)
            => await this.CreateAsync<CreateTeamDto, TeamDto, Domain.Entities.Team>(team,
                _teamRepository.AddTeamAsync, _teamValidator.ValidateTeamCreation);
    }
}
