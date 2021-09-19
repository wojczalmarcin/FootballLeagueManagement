using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("FL_LG_PlayerSuspensionLog")]
    public class PlayerSuspensionLog
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("PlayerId")]
        public int PlayerId { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        public virtual Member Player { get; set; }

    }
}
