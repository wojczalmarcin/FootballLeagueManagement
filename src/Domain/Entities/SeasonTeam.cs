using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Season with team many to many relation
    /// </summary>
    [Table("FL_TB_SeasonTeam")]
    public class SeasonTeam
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("SeasonId")]
        public int SeasonId { get; set; }

        [Column("TeamId")]
        public int TeamId { get; set; }

        public virtual Season Season { get; set; }

        public virtual Team Team { get; set; }
    }
}
