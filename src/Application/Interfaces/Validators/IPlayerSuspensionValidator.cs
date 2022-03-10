using Application.DTO;
using System.Collections.Generic;
using System.Net;

namespace Application.Interfaces.Validators
{
    public interface IPlayerSuspensionValidator
    {
        /// <summary>
        /// Validates player suspension log existence
        /// </summary>
        /// <param name="playerSuspension">player suspension</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayerSuspensionExistence(PlayerSuspensionDto playerSuspension);
    }
}
