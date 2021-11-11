using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Validators
{
    public interface IPlayerStatTypeValidator
    {
        /// <summary>
        /// Validates player stat type existence
        /// </summary>
        /// <param name="playersStatType">The players stat type</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidatePlayerStatTypeExistence(PlayerStatTypeDto playersStatType);

    }
}
