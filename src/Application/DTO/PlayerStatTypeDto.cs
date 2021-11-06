namespace Application.DTO
{
    public class PlayerStatTypeDto : IDtoWithId
    {
        public int Id { get; set; }

        public string StatName { get; set; }

        public bool IsGoal { get; set; }
    }
}
