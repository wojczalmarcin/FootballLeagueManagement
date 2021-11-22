using Application.DTO;
using System.Collections.Generic;
using System.Net;

namespace Application.Interfaces.Validators
{
    /// <summary>
    /// Member validator interface
    /// </summary>
    public interface IMemberValidator
    {
        /// <summary>
        /// Validates member existence
        /// </summary>
        /// <param name="member">member</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateMemberExistence(MemberDto member);

        /// <summary>
        /// Validates member editing
        /// </summary>
        /// <param name="team">The member</param>
        /// <param name="teamToEdit">The member to edit</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateMemberEdit(MemberEditDto member, MemberDto memberToEdit);

        /// <summary>
        /// Validates members existence
        /// </summary>
        /// <param name="members">The members</param>
        /// <returns>Validation result</returns>
        (HttpStatusCode statusCode, List<string> validationErrors) ValidateMembersExistence(IEnumerable<MemberDto> members);
    }
}
