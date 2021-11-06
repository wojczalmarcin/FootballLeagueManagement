using System;
using System.Collections.Generic;

namespace Application.DTO
{
    public class MatchDto : IDtoWithId
    {
        public int Id { get; set; }

        public bool IsFinished { get; set; }

        public DateTime? Date { get; set; }

        public virtual TeamDto TeamHome { get; set; }

        public virtual TeamDto TeamAway { get; set; }

        public virtual SeasonDto Season { get; set; }

        public virtual AddressDto Address { get; set; }

        public virtual MatchScoreDto MatchScore { get; set; }

        public virtual IEnumerable<MemberDto> HomePlayers { get; set; }

        public virtual IEnumerable<MemberDto> AwayPlayers { get; set; }
    }
}
