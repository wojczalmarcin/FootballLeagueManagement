using Application.DTO;
using Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Member
{
    /// <summary>
    /// Validates member data transfer object
    /// </summary>
    public class MemberValidator : Validator, IMemberValidator
    {
        /// <summary>
        /// Validates member existence
        /// </summary>
        /// <param name="member">member</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateMemberExistence(MemberDto member)
            => ValidateEntityExistence(member, "Season with given Id doesn't exist");

        /// <summary>
        /// Validates member editing
        /// </summary>
        /// <param name="team">The member</param>
        /// <param name="teamToEdit">The member to edit</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateMemberEdit(MemberDto member, MemberDto memberToEdit)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validation = this.ValidateMemberExistence(memberToEdit);
            if (validation.statusCode != HttpStatusCode.OK)
            {
                return validation;
            }
            return validation;
        }
    }
}
