using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class StadiumDto : IDtoWithId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfSeats { get; set; }

        public virtual AddressDto Address { get; set; }
    }
}
