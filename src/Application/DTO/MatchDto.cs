using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class MatchDto
    {
        public int Id { get; set; }

        public bool IsFinished { get; set; }
        public DateTime? Date { get; set; }

        public virtual TeamDto TeamHome { get; set; }

        public virtual TeamDto TeamAway { get; set; }

        public virtual SeasonDto Season { get; set; }

        public virtual AddressDto Address { get; set; }

        public virtual MatchScoreDto MatchScore { get; set; }
    }
}
