using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Player stats on the pitch
    /// </summary>
    [Table("FL_LG_PlayerStatsLog")]
    public class PlayerStatsLog
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("MatchId")]
        public int MatchId { get; set; }

        [Column("PlayerId")]
        public int PlayerId { get; set; }

        [Column("TeamId")]
        public int TeamId { get; set; }

        [Column("StatTypeId")]
        public int StatTypeId { get; set; }

        [Column("StartMinute")]
        public int? StartMinute { get; set; }

        public virtual Member Player { get; set; }

        public virtual Match Match { get; set; }

        public virtual Team Team { get; set; }

        public virtual PlayerStatType StatType { get; set; }
    }
}
