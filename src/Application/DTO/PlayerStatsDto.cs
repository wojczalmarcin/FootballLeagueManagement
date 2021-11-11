namespace Application.DTO
{
    public class PlayerStatsDto : CreatePlayerStatsDto, IDtoWithId
    {
        public int Id { get; set; }

        public PlayerStatTypeDto StatType { get; set; }

        public MemberDto Player { get; set; }

        public TeamDto Team { get; set; }
    }
}
