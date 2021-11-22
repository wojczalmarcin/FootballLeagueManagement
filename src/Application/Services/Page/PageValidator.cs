using Application.Interfaces.Validators;
using System.Collections.Generic;
using System.Net;

namespace Application.Services.Page
{
    /// <summary>
    /// The page validator
    /// </summary>
    public class PageValidator : IPageValidator
    {
        /// <summary>
        /// Validates page existence
        /// </summary>
        /// <param name="page">The page</param>
        /// <param name="numberOfMembers">The number of the members</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidatePageExistence((int size, int number) page, int numberOfMembers)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            if(page.number < 1)
            {
                validationErrors.Add("Page number must be higher than zero");
                statusCode = HttpStatusCode.BadRequest;
            }
            if (page.size < 1)
            {
                validationErrors.Add("Page size must be higher than zero");
                statusCode = HttpStatusCode.BadRequest;
            }
            if (page.size > 1 && (page.size * page.number >= numberOfMembers + page.size))
            {
               validationErrors.Add("Page with this number does not exist");
               statusCode = HttpStatusCode.BadRequest;
            }

            return (statusCode, validationErrors);
        }
        
    }
}
