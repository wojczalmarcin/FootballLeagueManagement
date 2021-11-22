using Application.DTO;
using Application.DTO.Edit;
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
    public class MatchService : Service, IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        private readonly ITeamRepository _teamRepository;

        private readonly ISeasonRepository _seasonRepository;

        private readonly IMatchValidator _matchValidator;

        private readonly ISeasonValidator _seasonValidator;

        private MatchLogic matchLogic;

        public MatchService(IMatchRepository matchRepository, 
            IMapper mapper, 
            ITeamRepository teamRepository,
            ISeasonRepository seasonRepository,
            IMatchValidator matchValidator,
            ISeasonValidator seasonValidator)
            : base (mapper)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _seasonRepository = seasonRepository;
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
            => await GetByIdAsync<MatchDto, Domain.Entities.Match> (matchId, _matchRepository.GetMatchByIdAsync, 
                _matchValidator.ValidateMatchExistence);

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

        /// <summary>
        /// Creates new match
        /// </summary>
        /// <param name="match">The match</param>
        /// <returns>Response data with created match</returns>
        public async Task<ResponseData<MatchDto>> CreateMatchAsync(CreateMatchDto match)
            => await this.CreateAsync<CreateMatchDto, MatchDto, Domain.Entities.Match>(match,
                _matchRepository.AddMatchAsync, _matchValidator.ValidateMatchCreation);

        /// <summary>
        /// Deletes match
        /// </summary>
        /// <param name="matchId">The match id</param>
        /// <returns>Response data with deleted match</returns>
        public async Task<ResponseData<MatchDto>> DeleteMatchAsync(int matchId)
            => await this.DeleteAsync<MatchDto, Domain.Entities.Match>(matchId, _matchRepository.GetMatchByIdAsync,
                _matchRepository.DeleteMatchAsync, _matchValidator.ValidateMatchDeletion);

        /// <summary>
        /// Edits match
        /// </summary>
        /// <param name="match">The match</param>
        /// <returns>Response data with edited match</returns>
        public async Task<ResponseData<MatchDto>> EditMatchAsync(EditMatchDto match)
        {
            var responseData = new ResponseData<MatchDto>();

            if(match.Id == 0)
            {
                responseData.ResponseStatus = HttpStatusCode.BadRequest;
                responseData.ValidationErrors.Add("The match id was not provided");
                return responseData;
            }

            var existingMatch = _mapper.Map<MatchDto>(await _matchRepository.GetMatchByIdAsync(match.Id));

            var validateEdition =  _matchValidator.ValidateMatchEdition(match, existingMatch);

            responseData.ResponseStatus = validateEdition.statusCode;
            responseData.ValidationErrors = validateEdition.validationErrors;

            if (validateEdition.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            existingMatch.IsFinished = match.IsFinished;
            existingMatch.Date = match.Date;

            var matchEdited = await _matchRepository.EditMatchAsync(_mapper.Map<Domain.Entities.Match>(existingMatch));

            if (matchEdited)
            {
                responseData.Data = existingMatch;
            }
            else
            {
                responseData.ResponseStatus = HttpStatusCode.InternalServerError;
                responseData.ValidationErrors.Add("There was a problem with creating match");
            }

            return responseData;
        }

        /// <summary>
        /// Sets finish flag of the match to true
        /// </summary>
        /// <param name="matchId">The match Id</param>
        /// <returns>Response data with finished match</returns>
        public async Task<ResponseData<MatchDto>> FinishTheMatch(int matchId)
        {
            var responseData = new ResponseData<MatchDto>();

            var match = _mapper.Map<MatchDto>(await _matchRepository.GetMatchByIdAsync(matchId));

            var validate = _matchValidator.ValidateMatchFinishing(match);

            responseData.ResponseStatus = validate.statusCode;
            responseData.ValidationErrors = validate.validationErrors;

            if (validate.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            match.IsFinished = true;
            
            if(await _matchRepository.EditMatchAsync(_mapper.Map<Domain.Entities.Match> (match)))
            {
                responseData.Data = match;
            }
            else
            {
                responseData.ResponseStatus = HttpStatusCode.InternalServerError;
                responseData.ValidationErrors.Add("There was a problem with finishing the match");
            }

            return responseData;
        }
    }
}
