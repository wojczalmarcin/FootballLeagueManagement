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
    /// Season
    /// </summary>
    [Table("FL_TB_Season")]
    public class Season
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime? EndDate { get; set; }

        [Column("Sponsor")]
        public string Sponsor { get; set; }
    }
}
