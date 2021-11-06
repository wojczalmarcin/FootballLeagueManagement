using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    /// <summary>
    /// The member data transfer object
    /// </summary>
    public class MemberDto : IDtoWithId
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual TeamDto Team { get; set; }
        public virtual MemberRoleDto MemberRole { get; set; }
    }
}
