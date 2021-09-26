using Application.DTO;
using System.Collections.Generic;
using System.Net;

namespace Application.Interfaces.Validators
{
    /// <summary>
    /// Match validator interface
    /// </summary>
    public interface IMatchValidator
    {
        /// <summary>
        /// Validates match existence
        /// </summary>
        /// <param name="match">match data transfer object</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateMatchExistence(MatchDto match);
    }
}
