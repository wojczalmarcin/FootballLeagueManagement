using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreatePlayerStatsDto
    {
        public int MatchId { get; set; }

        public int PlayerId { get; set; }

        public int TeamId { get; set; }

        public int StatTypeId { get; set; }

        public int? StartMinute { get; set; }
    }
}
