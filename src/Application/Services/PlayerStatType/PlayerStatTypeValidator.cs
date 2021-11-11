using Application.DTO;
using Application.Interfaces.Validators;
using System.Collections.Generic;
using System.Net;

namespace Application.Services.PlayerStatType
{
    public class PlayerStatTypeValidator : Validator, IPlayerStatTypeValidator
    {
        /// <summary>
        /// Validates player stat type existence
        /// </summary>
        /// <param name="playersStatType">The players stat type</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayerStatTypeExistence(PlayerStatTypeDto playersStatType)
            => ValidateEntityExistence(playersStatType, "Player stat type does not exist");
    }
}
