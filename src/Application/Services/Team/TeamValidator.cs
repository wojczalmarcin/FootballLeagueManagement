using Application.DTO;
using Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Team
{
    /// <summary>
    /// The team validator
    /// </summary>
    public class TeamValidator : ITeamValidator
    {
        /// <summary>
        /// Validates team existence
        /// </summary>
        /// <param name="team">team</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateTeamExistence(TeamDto team)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            if (team == null)
            {
                statusCode = HttpStatusCode.NotFound;
                validationErrors.Add("Team with given Id doesn't exist");
                return (statusCode, validationErrors);
            }

            return (statusCode, validationErrors);
        }
    }
}
