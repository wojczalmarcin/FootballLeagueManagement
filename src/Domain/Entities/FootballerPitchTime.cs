using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Player time on the pitch
    /// </summary>
    [Table("FL_TB_FootballerPitchTime")]
    public class FootballerPitchTime
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("PlayerId")]
        public int PlayerId { get; set; }

        [Column("AppearanceMinute")]
        public int AppearanceMinute { get; set; }

        [Column("MinutesPlayed")]
        public int MinutesPlayed { get; set; }

        public virtual Member Player { get; set; }
    }
}
