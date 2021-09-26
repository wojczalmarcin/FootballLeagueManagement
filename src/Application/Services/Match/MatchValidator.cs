using Application.DTO;
using Application.Interfaces.Validators;
using System.Collections.Generic;
using System.Net;

namespace Application.Services.Match
{
    /// <summary>
    /// The match validator
    /// </summary>
    public class MatchValidator : IMatchValidator
    {
        /// <summary>
        /// Validates match existence
        /// </summary>
        /// <param name="match">match data transfer object</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateMatchExistence(MatchDto match)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            if (match==null)
            {
                statusCode = HttpStatusCode.NotFound;
                validationErrors.Add("Match with given Id doesn't exist");
                return (statusCode, validationErrors);
            }

            if(match.TeamAway==null)
            {
                statusCode = HttpStatusCode.BadRequest;
                validationErrors.Add("Away team doesn't exist");
            }

            if (match.TeamHome == null)
            {
                statusCode = HttpStatusCode.BadRequest;
                validationErrors.Add("Home team doesn't exist");
            }

            return (statusCode, validationErrors);
        }
    }
}
