using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateMatchDto
    {
        public DateTime? Date { get; set; }

        public int TeamHomeId { get; set; }

        public int TeamAwayId { get; set; }

        public int SeasonId { get; set; }
    }
}
