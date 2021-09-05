using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Type of address
    /// </summary>
    public class AddressType
    {
        public int Id { get; set; }

        public bool IsStadium { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<Address> Addresses { get; set; }
    }
}
