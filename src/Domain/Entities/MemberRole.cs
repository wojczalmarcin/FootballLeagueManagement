using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Member role
    /// </summary>
    public class MemberRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<Member> Members { get; set; }
    }
}
