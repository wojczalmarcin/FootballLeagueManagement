using System.Collections.Generic;
using System.Net;

namespace Application.Interfaces.Validators
{
    /// <summary>
    /// The page validator interface
    /// </summary>
    public interface IPageValidator
    {
        /// <summary>
        /// Validates page existence
        /// </summary>
        /// <param name="page">The page</param>
        /// <param name="numberOfMembers">The number of the members</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidatePageExistence((int size, int number) page, int numberOfMembers);
    }
}
