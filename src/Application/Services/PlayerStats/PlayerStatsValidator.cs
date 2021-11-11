using Application.DTO;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.PlayerStats
{
    public class PlayerStatsValidator : Validator, IPlayerStatsValidator
    {
        
        private readonly IMatchRepository _matchRepository;

        private readonly IMemberRepository _memberRepository;

        private readonly ITeamRepository _teamRepository;

        private readonly IPlayerStatTypeRepository _playerStatTypeRepository;

        private readonly IMatchValidator _matchValidator;

        private readonly IMemberValidator _memberValidator;

        private readonly ITeamValidator _teamValidator;

        private readonly IPlayerStatTypeValidator _playerStatTypeValidator;

        private readonly IMapper _mapper;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="matchRepository">The match repository</param>
        /// <param name="memberRepository">The member repository</param>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="playerStatTypeRepository">The player stat type repository</param>
        /// <param name="matchValidator">The match validator</param>
        /// <param name="memberValidator">The member validator</param>
        /// <param name="teamValidator">The team validator</param>
        /// <param name="playerStatTypeValidator">The stat type validator</param>
        /// <param name="mapper">The mapper</param>
        public PlayerStatsValidator(IMatchRepository matchRepository, 
            IMemberRepository memberRepository, 
            ITeamRepository teamRepository, 
            IPlayerStatTypeRepository playerStatTypeRepository, 
            IMatchValidator matchValidator, 
            IMemberValidator memberValidator, 
            ITeamValidator teamValidator, 
            IPlayerStatTypeValidator playerStatTypeValidator,
            IMapper mapper)
        {
            _matchRepository = matchRepository;
            _memberRepository = memberRepository;
            _teamRepository = teamRepository;
            _playerStatTypeRepository = playerStatTypeRepository;
            _matchValidator = matchValidator;
            _memberValidator = memberValidator;
            _teamValidator = teamValidator;
            _playerStatTypeValidator = playerStatTypeValidator;
            _mapper = mapper;
        }

        /// <summary>
        /// Validates players stat existence
        /// </summary>
        /// <param name="playerStats">The player stats</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayerStatsExistence(PlayerStatsDto playerStats)
            => ValidateEntityExistence(playerStats, "This player statistic does not exist");

        /// <summary>
        /// Validates players stats existence
        /// </summary>
        /// <param name="playersStats">The players stats</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayersStatsExistence(IEnumerable<PlayerStatsDto> playersStats)
            => ValidateEntitiesExistence(playersStats, "Players statistics do not exist");

        /// <summary>
        /// Validates player stats creation
        /// </summary>
        /// <param name="playerStats">The player stats to create</param>
        /// <returns>Validation result</returns>
        public async Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidatePlayerStatsCreationAsync(CreatePlayerStatsDto playerStats)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            var match = _matchRepository.GetMatchByIdAsync(playerStats.MatchId);
            var player = _memberRepository.GetPlayerByIdAsync(playerStats.PlayerId);
            var team = _teamRepository.GetTeamByIdAsync(playerStats.TeamId);
            var statType = _playerStatTypeRepository.GetPlayerStatTypeByIdAsync(playerStats.StatTypeId);
           

            var validateMatch = _matchValidator.ValidateMatchExistence(_mapper.Map<MatchDto>(await match));
            var validatePlayer = _memberValidator.ValidateMemberExistence(_mapper.Map<MemberDto>(await player));
            var validateTeam = _teamValidator.ValidateTeamExistence(_mapper.Map<TeamDto>(await team));
            var validateStatType = _playerStatTypeValidator.ValidatePlayerStatTypeExistence(_mapper.Map<PlayerStatTypeDto>(await statType));

            if(validateMatch.statusCode != HttpStatusCode.OK)
            {
                statusCode = validateMatch.statusCode;
                validationErrors.AddRange(validateMatch.validationErrors);
            }
            if (validatePlayer.statusCode != HttpStatusCode.OK)
            {
                statusCode = validatePlayer.statusCode;
                validationErrors.AddRange(validatePlayer.validationErrors);
            }
            if (validateTeam.statusCode != HttpStatusCode.OK)
            {
                statusCode = validateTeam.statusCode;
                validationErrors.AddRange(validateTeam.validationErrors);
            }
            if (validateStatType.statusCode != HttpStatusCode.OK)
            {
                statusCode = validateStatType.statusCode;
                validationErrors.AddRange(validateStatType.validationErrors);
            }

            return (statusCode, validationErrors);
        }

        /// <summary>
        /// Valdiates player stat deletion
        /// </summary>
        /// <param name="playerStats">The player stats</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayerStatsDeletion(PlayerStatsDto playerStats)
        {

            var validatePlayerStatsExistence = this.ValidatePlayerStatsExistence(playerStats);
            var statusCode = validatePlayerStatsExistence.statusCode;
            var validationErrors = validatePlayerStatsExistence.validationErrors;

            return (statusCode, validationErrors);
        }

    }
}
