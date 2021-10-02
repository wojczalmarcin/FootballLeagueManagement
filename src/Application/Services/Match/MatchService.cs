using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using Application.Services.Season;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.Match
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        private readonly ITeamRepository _teamRepository;

        private readonly ISeasonRepository _seasonRepository;

        private readonly IMapper _mapper;

        private readonly IMatchValidator _matchValidator;

        private readonly ISeasonValidator _seasonValidator;

        private MatchLogic matchLogic;

        public MatchService(IMatchRepository matchRepository, 
            IMapper mapper, 
            ITeamRepository teamRepository,
            ISeasonRepository seasonRepository,
            IMatchValidator matchValidator,
            ISeasonValidator seasonValidator)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _seasonRepository = seasonRepository;
            _mapper = mapper;
            _matchValidator = matchValidator;
            matchLogic = new MatchLogic();
            _seasonValidator = seasonValidator;
        }

        /// <summary>
        /// Gets match data by match id
        /// </summary>
        /// <param name="matchId">Match id</param>
        /// <returns>Response with match data</returns>
        public async Task<ResponseData<MatchDto>> GetMatchDataById(int matchId)
        {
            var responseData = new ResponseData<MatchDto>();

            var match = _mapper.Map<MatchDto>(await _matchRepository.GetMatchByIdAsync(matchId));
            var matchValidation = _matchValidator.ValidateMatchExistence(match);

            responseData.ResponseStatus = matchValidation.statusCode;
            responseData.ValidationErrors = matchValidation.validationErrors;

            if(matchValidation.statusCode == HttpStatusCode.OK)
            {
                responseData.Data = match;
            }

            return responseData;
        }

        /// <summary>
        /// Gets matches by season id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data with collection of <see cref="MatchDto"/></returns>
        public async Task<ResponseData<IEnumerable<MatchDto>>> GetMatchesDataBySeasonId(int seasonId)
        {
            var responseData = new ResponseData<IEnumerable<MatchDto>>();
            var season = _mapper.Map<SeasonDto>(await _seasonRepository.GetSeasonByIdAsync(seasonId));
            var seasonValidation = _seasonValidator.ValidateSeasonExistence(season);

            responseData.ResponseStatus = seasonValidation.statusCode;
            responseData.ValidationErrors = seasonValidation.validationErrors;

            if (seasonValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            var matches = _mapper.Map<IEnumerable<MatchDto>> (await _matchRepository.GetMatchesBySeasonIdAsync(seasonId));
            responseData.Data = matches;
            return responseData;
        }

        /// <summary>
        /// Gets season table by season id
        /// </summary>
        /// <param name="seasonId">Season id</param>
        /// <returns>Response data with collection of <see cref="TeamStatisticsDto"/></returns>
        public async Task<ResponseData<IEnumerable<TeamStatisticsDto>>> GetSeasonTable(int seasonId)
        {
            var responseData = new ResponseData<IEnumerable<TeamStatisticsDto>>();
            var season =  _mapper.Map<SeasonDto>( await _seasonRepository.GetSeasonByIdAsync(seasonId));
            var seasonValidation = _seasonValidator.ValidateSeasonExistence(season);

            responseData.ResponseStatus = seasonValidation.statusCode;
            responseData.ValidationErrors = seasonValidation.validationErrors;

            if (seasonValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            var matches = _mapper.Map<IEnumerable<MatchDto>>(await _matchRepository.GetMatchesBySeasonIdAsync(seasonId));
            var teamStatistics = _mapper.Map<IEnumerable<TeamStatisticsDto>>(await _teamRepository.GetTeamsBySeasonIdAsync(seasonId));

            teamStatistics = await matchLogic.CreateSeasonTableAsync(teamStatistics, matches);
            responseData.Data = teamStatistics;
            return responseData;
        }
    }
}
