using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Match
{
    public class MatchValidator
    {
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
