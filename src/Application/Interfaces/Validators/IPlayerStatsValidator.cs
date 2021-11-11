using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Validators
{
    public interface IPlayerStatsValidator
    {
        /// <summary>
        /// Validates players stat existence
        /// </summary>
        /// <param name="playerStats">The player stats</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayerStatsExistence(PlayerStatsDto playerStats);

        /// <summary>
        /// Validates players stats existence
        /// </summary>
        /// <param name="playersStats">The players stats</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayersStatsExistence(IEnumerable<PlayerStatsDto> playersStats);

        /// <summary>
        /// Validates player stats creation
        /// </summary>
        /// <param name="playerStats">The player stats to create</param>
        /// <returns>Validation result</returns>
        Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidatePlayerStatsCreationAsync(CreatePlayerStatsDto playerStats);

        /// <summary>
        /// Valdiates player stat deletion
        /// </summary>
        /// <param name="playerStats">The player stats</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayerStatsDeletion(PlayerStatsDto playerStats);
    }
}
