using Application.DTO;
using Application.DTO.Edit;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.Match
{
    /// <summary>
    /// The match validator
    /// </summary>
    public class MatchValidator : Validator, IMatchValidator
    {

        private readonly ITeamValidator _teamValidator;

        private readonly ITeamRepository _teamRepository;

        private readonly ISeasonRepository _seasonRepository;

        private readonly ISeasonValidator _seasonValidator;

        private readonly IMapper _mapper;

        private readonly IMatchRepository _matchRepository;

        public MatchValidator(ITeamValidator teamValidator, 
            ITeamRepository teamRepository, 
            ISeasonRepository seasonRepository, 
            ISeasonValidator seasonValidator, 
            IMapper mapper, 
            IMatchRepository matchRepository)
        {
            _teamValidator = teamValidator;
            _teamRepository = teamRepository;
            _seasonRepository = seasonRepository;
            _seasonValidator = seasonValidator;
            _mapper = mapper;
            _matchRepository = matchRepository;
        }

        /// <summary>
        /// Validates match existence
        /// </summary>
        /// <param name="match">match data transfer object</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateMatchExistence(MatchDto match)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            var existence = ValidateEntityExistence(match, "Match with given Id doesn't exist");
            if (existence.statusCode != HttpStatusCode.OK)
            {
                return existence;
            }

            if(match.TeamAway==null)
            {
                statusCode = HttpStatusCode.BadRequest;
                validationErrors.Add("Away team doesn't exist");
            }

            if (match.TeamHome == null)
            {
                statusCode = HttpStatusCode.BadRequest;
                validationErrors.Add("Home team doesn't exist");
            }

            return (statusCode, validationErrors);
        }

        /// <summary>
        /// Validates match creation
        /// </summary>
        /// <param name="match">The match to create</param>
        /// <param name="matchesInSeason">The matches in season</param>
        /// <returns>Validation result</returns>
        public async Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateMatchCreation(CreateMatchDto match)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            var season = _mapper.Map<SeasonDto>(await _seasonRepository.GetSeasonByIdAsync(match.SeasonId));
            var seasonExistenceValidation = _seasonValidator.ValidateSeasonExistence(season);

            if (seasonExistenceValidation.statusCode != HttpStatusCode.OK)
            {
                statusCode = seasonExistenceValidation.statusCode;
                validationErrors = seasonExistenceValidation.validationErrors;
            }

            var matchesInSeason = _mapper.Map<IEnumerable<MatchDto>>(await _matchRepository.GetMatchesBySeasonIdAsync(match.SeasonId));
            var teamHome = _mapper.Map<TeamDto>(await _teamRepository.GetTeamByIdAsync(match.TeamHomeId));
            var teamAway = _mapper.Map<TeamDto>(await _teamRepository.GetTeamByIdAsync(match.TeamAwayId));

            var teamHomeValidation = _teamValidator.ValidateTeamExistence(teamHome);

            if (teamHomeValidation.statusCode != HttpStatusCode.OK)
            {
                statusCode = teamHomeValidation.statusCode;
                validationErrors.AddRange(teamHomeValidation.validationErrors);
            }

            var teamAwayValidation = _teamValidator.ValidateTeamExistence(teamAway);

            if (teamAwayValidation.statusCode != HttpStatusCode.OK)
            {
                statusCode = teamAwayValidation.statusCode;
                validationErrors.AddRange(teamAwayValidation.validationErrors);
            }

            var matchesInSeasonList = matchesInSeason.ToList();

            foreach(var matchInSeason in matchesInSeasonList)
            {
                if(matchInSeason.TeamHome.Id == match.TeamHomeId
                    && matchInSeason.TeamAway.Id == match.TeamAwayId)
                {
                    statusCode = HttpStatusCode.BadRequest;
                    validationErrors.Add("This match already exists");
                    return (statusCode, validationErrors);
                }
            }

            return (statusCode, validationErrors);
        }

        /// <summary>
        /// Valdiates match deletion
        /// </summary>
        /// <param name="match">The match</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateMatchDeletion(MatchDto match)
        {

            var validateMatchExistence = this.ValidateMatchExistence(match);
            var statusCode = validateMatchExistence.statusCode;
            var validationErrors = validateMatchExistence.validationErrors;

            if (validateMatchExistence.statusCode != HttpStatusCode.OK)
            {
                return (statusCode, validationErrors);
            }

            if (match.IsFinished)
            {
                statusCode = HttpStatusCode.BadRequest;
                validationErrors.Add("Finished match cannot be deleted");
            }

            if(match.Date <= DateTime.Now)
            {
                statusCode = HttpStatusCode.BadRequest;
                validationErrors.Add("Match which has already started cannot be deleted");
            }

            return (statusCode, validationErrors);
        }

        /// <summary>
        /// Validates match edit
        /// </summary>
        /// <param name="editedMatch">The edited match</param>
        /// <param name="existingMatch">The existing match</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateMatchEdition(EditMatchDto editedMatch, MatchDto existingMatch)
        {
            var validateMatchExistence = this.ValidateMatchExistence(existingMatch);
            var statusCode = validateMatchExistence.statusCode;
            var validationErrors = validateMatchExistence.validationErrors;

            if (validateMatchExistence.statusCode != HttpStatusCode.OK)
            {
                return (statusCode, validationErrors);
            }

            if (existingMatch.Id != editedMatch.Id)
            {
                statusCode = HttpStatusCode.InternalServerError;
                validationErrors.Add("Existing match id does not match to edited one");
            }

            return (statusCode, validationErrors);

        }
    }
}
