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
    public class TeamValidator : Validator, ITeamValidator
    {
        /// <summary>
        /// Validates team existence
        /// </summary>
        /// <param name="team">team</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateTeamExistence(TeamDto team)
            => ValidateEntityExistence(team, "Team with given Id doesn't exist");

        /// <summary>
        /// Validates team editing
        /// </summary>
        /// <param name="team">The team</param>
        /// <param name="teamToEdit">The team to edit</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateTeamEdit(TeamDto team, TeamDto teamToEdit)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validation = this.ValidateTeamExistence(teamToEdit);
            if (validation.statusCode != HttpStatusCode.OK)
            {
                return validation;
            }
            return validation;
        }
    }
}
