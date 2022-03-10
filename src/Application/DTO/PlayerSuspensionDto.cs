using System;

namespace Application.DTO
{
    public class PlayerSuspensionDto : IDtoWithId
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MemberDto Player { get; set; }
    }
}
