using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateSeasonDto
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Sponsor { get; set; }
    }
}
