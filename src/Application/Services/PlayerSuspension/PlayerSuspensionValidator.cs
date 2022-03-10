using Application.DTO;
using Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PlayerSuspension
{
    /// <summary>
    /// The player suspension validator
    /// </summary>
    public class PlayerSuspensionValidator : Validator, IPlayerSuspensionValidator
    {
        /// <summary>
        /// Validates player suspension log existence
        /// </summary>
        /// <param name="playerSuspension">player suspension</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayerSuspensionExistence(PlayerSuspensionDto playerSuspension)
            => ValidateEntityExistence(playerSuspension, "This player suspension does not exist");
    }
}
