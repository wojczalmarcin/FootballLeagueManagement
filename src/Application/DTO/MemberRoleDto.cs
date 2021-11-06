using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class MemberRoleDto : IDtoWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
