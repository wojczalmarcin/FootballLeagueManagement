using Application.DTO;
using System.Collections.Generic;
using System.Net;

namespace Application.Interfaces.Validators
{
    /// <summary>
    /// The member role validator interface
    /// </summary>
    public interface IMemberRoleValidator
    {
        /// <summary>
        /// Validates member existence
        /// </summary>
        /// <param name="memberRole">The member role</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateMemberRoleExistence(MemberRoleDto memberRole);
    }
}
