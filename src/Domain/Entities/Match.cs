using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Football match event
    /// </summary>
    public class Match
    {
        public int Id { get; set; }

        public int TeamHomeId { get; set; }

        public int TeamAwayId { get; set; }

        public int AddressId { get; set; }

        public DateTime Date { get; set; }

        public virtual IEnumerable<Team> Teams { get; set; }

        public virtual Address Address { get; set; }
    }
}
