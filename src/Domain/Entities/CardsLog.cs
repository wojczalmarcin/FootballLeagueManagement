using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Player's cards quantity
    /// </summary>
    public class CardsLog
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int CurrentYellowCars { get; set; }

        public virtual Member Player { get; set; }
    }
}
