using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Validators
{
    /// <summary>
    /// The team validator interface
    /// </summary>
    public interface ITeamValidator
    {
        /// <summary>
        /// Validates team existence
        /// </summary>
        /// <param name="team">team</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateTeamExistence(TeamDto team);

        /// <summary>
        /// Validates team editing
        /// </summary>
        /// <param name="team">The team</param>
        /// <param name="teamToEdit">The team to edit</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateTeamEdit(TeamDto team, TeamDto teamToEdit);
    }
}
