using Application.DTO;
using Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MemberRole
{
    /// <summary>
    /// The member role validator
    /// </summary>
    public class MemberRoleValidator : Validator, IMemberRoleValidator
    {
        /// <summary>
        /// Validates member existence
        /// </summary>
        /// <param name="memberRole">The member role</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateMemberRoleExistence(MemberRoleDto memberRole)
            => ValidateEntityExistence(memberRole, "Member role with given Id doesn't exist");
    }
}
