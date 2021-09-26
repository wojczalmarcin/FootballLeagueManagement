using Application.DTO;
using Application.Interfaces.Validators;
using System.Collections.Generic;
using System.Net;

namespace Application.Services.Season
{
    /// <summary>
    /// Validates season data transfer object
    /// </summary>
    public class SeasonValidator : ISeasonValidator
    {
        /// <summary>
        /// Validates season existence
        /// </summary>
        /// <param name="season">season</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateSeasonExistence(SeasonDto season)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            if (season == null)
            {
                statusCode = HttpStatusCode.NotFound;
                validationErrors.Add("Season with given Id doesn't exist");
                return (statusCode, validationErrors);
            }

            return (statusCode, validationErrors);
        }
    }
}
