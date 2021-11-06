using Application.DTO;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.PlayerStats
{
    public class PlayerStatsService : Service, IPlayerStatsService
    {
        private readonly IPlayerStatsLogRepository _playerStatsLogRepository;

        private readonly IPlayerStatsValidator _playerStatsValidator;

        private readonly IMatchValidator _matchValidator;

        private readonly IMatchRepository _matchRepository;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="playerStatsLogRepository">The player stats log repository</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="playerStatsValidator">The player stats validator</param>
        public PlayerStatsService(IPlayerStatsLogRepository playerStatsLogRepository,
           IMapper mapper,
           IPlayerStatsValidator playerStatsValidator,
           IMatchValidator matchValidator,
           IMatchRepository matchRepository)
           : base(mapper)
        {
            _playerStatsLogRepository = playerStatsLogRepository;
            _playerStatsValidator = playerStatsValidator;
            _matchValidator = matchValidator;
            _matchRepository = matchRepository;
        }

        /// <summary>
        /// Gets players stats data by match id
        /// </summary>
        /// <param name="matchId">Match id</param>
        /// <returns>Response with players stats data</returns>
        public async Task<ResponseData<IEnumerable<PlayerStatsDto>>> GetPlayersStatsByMatchId(int matchId)
        {
            var matchDto = _mapper.Map<MatchDto>(await _matchRepository.GetMatchByIdAsync(matchId));
            var validation = _matchValidator.ValidateMatchExistence(matchDto);

            var responseData = new ResponseData<IEnumerable<PlayerStatsDto>>();

            if (validation.statusCode != HttpStatusCode.OK)
            {
                responseData.ResponseStatus = validation.statusCode;
                responseData.ValidationErrors = validation.validationErrors;
                return responseData;
            }
            var playersStats = _mapper.Map<IEnumerable<PlayerStatsDto>> (await _playerStatsLogRepository.GetPlayersStatsLogByMatchIdAsync(matchId));

            validation = _playerStatsValidator.ValidatePlayersStatsExistence(playersStats);

            if (validation.statusCode != HttpStatusCode.OK)
            {
                responseData.ResponseStatus = validation.statusCode;
                responseData.ValidationErrors = validation.validationErrors;
                return responseData;
            }

            responseData.ResponseStatus = HttpStatusCode.OK;
            responseData.Data = playersStats;
            return responseData;
        }
    }
}
