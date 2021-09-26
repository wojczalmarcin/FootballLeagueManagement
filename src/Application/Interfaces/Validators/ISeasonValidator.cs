using Application.DTO;
using System.Collections.Generic;
using System.Net;

namespace Application.Interfaces.Validators
{
    /// <summary>
    /// Season validator interface
    /// </summary>
    public interface ISeasonValidator
    {
        /// <summary>
        /// Validates season existence
        /// </summary>
        /// <param name="season">season</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateSeasonExistence(SeasonDto season);
    }
}
