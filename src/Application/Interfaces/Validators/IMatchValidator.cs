using Application.DTO;
using Application.DTO.Edit;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

        /// <summary>
        /// Validates match creation
        /// </summary>
        /// <param name="match">The match to create</param>
        /// <param name="matchesInSeason">The matches in season</param>
        /// <returns>Validation result</returns>
        Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateMatchCreation(CreateMatchDto match);

        /// <summary>
        /// Valdiates match deletion
        /// </summary>
        /// <param name="match">The match</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateMatchDeletion(MatchDto match);

        /// <summary>
        /// Validates match edit
        /// </summary>
        /// <param name="editedMatch">The edited match</param>
        /// <param name="existingMatch">The existing match</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateMatchEdition(EditMatchDto editedMatch, MatchDto existingMatch);

    }
}
