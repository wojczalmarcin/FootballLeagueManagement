using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Football team
    /// </summary>
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address{ get; set; }
    }
}
