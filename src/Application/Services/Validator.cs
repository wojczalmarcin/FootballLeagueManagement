using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Application.Services
{
    /// <summary>
    /// The validator class
    /// </summary>
    public abstract class Validator
    {
        /// <summary>
        /// Validates if entity exists based on given dto of this entity
        /// </summary>
        /// <param name="dto">The dto</param>
        /// <param name="message">the error message</param>
        /// <returns>Validation result</returns>
        protected (HttpStatusCode statusCode, List<string> validationErrors) ValidateEntityExistence(IDtoWithId dto, string message)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            if (dto == null)
            {
                statusCode = HttpStatusCode.NotFound;
                validationErrors.Add(message);
                return (statusCode, validationErrors);
            }

            return (statusCode, validationErrors);
        }

        /// <summary>
        /// Validates if entity exists based on given dto of this entity
        /// </summary>
        /// <param name="dto">The dto</param>
        /// <param name="message">the error message</param>
        /// <returns>Validation result</returns>
        protected (HttpStatusCode statusCode, List<string> validationErrors) ValidateEntitiesExistence(IEnumerable<IDtoWithId> dtoList, string message)
        {
            var statusCode = HttpStatusCode.OK;
            var validationErrors = new List<string>();

            if (dtoList == null || !dtoList.Any())
            {
                statusCode = HttpStatusCode.NotFound;
                validationErrors.Add(message);
                return (statusCode, validationErrors);
            }

            return (statusCode, validationErrors);
        }
    }
}
