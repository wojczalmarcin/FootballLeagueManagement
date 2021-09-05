using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Member of the league
    /// </summary>
    public class Member
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int MemberRoleId { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
        public virtual MemberRole MemberRole { get; set; }
    }
}
