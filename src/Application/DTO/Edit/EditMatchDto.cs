using System;

namespace Application.DTO.Edit
{
    public class EditMatchDto : IDtoWithId
    {
        public int Id { get; set; }

        public bool IsFinished { get; set; }

        public DateTime? Date { get; set; }
    }
}
