using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Player stats on the pitch
    /// </summary>
    public class PlayerStats
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int Goals { get; set; }

        public int OwnGoals { get; set; }

        public int Assists { get; set; }

        public int YellowCards { get; set; }

        public int RedCars { get; set; }

        public virtual Member Player { get; set; }
    }
}
