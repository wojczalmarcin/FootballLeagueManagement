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
        /// Validates players stats existence
        /// </summary>
        /// <param name="playersStats">The players stats</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayersStatsExistence(IEnumerable<PlayerStatsDto> playersStats);
    }
}
