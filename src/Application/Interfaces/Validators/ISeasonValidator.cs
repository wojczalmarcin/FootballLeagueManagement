using Application.DTO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

        /// <summary>
        /// Validates season editing
        /// </summary>
        /// <param name="season">The season</param>
        /// <param name="seasonToEdit">The season to edit</param>
        /// <returns>Validation result</returns>
        Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateSeasonEditAsync(SeasonDto season, SeasonDto seasonToEdit);

        /// <summary>
        /// Validates season creation
        /// </summary>
        /// <param name="season">Season to add</param>
        /// <returns>Validation result</returns>
        Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateSeasonCreation(CreateSeasonDto season);
    }
}
