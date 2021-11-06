using Application.DTO;
using Application.Interfaces.Validators;
using System.Collections.Generic;
using System.Net;

namespace Application.Services.PlayerStats
{
    public class PlayerStatsValidator : Validator, IPlayerStatsValidator
    {
        /// <summary>
        /// Validates players stats existence
        /// </summary>
        /// <param name="playersStats">The players stats</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayersStatsExistence(IEnumerable<PlayerStatsDto> playersStats)
            => ValidateEntitiesExistence(playersStats, "Players statistics do not exist");
    }
}
