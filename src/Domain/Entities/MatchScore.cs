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
    /// League table VIEW!!!
    /// </summary>
    [Table("FL_VW_MatchScore")]
    public class MatchScore
    {
        [Key]
        [Column("MatchId")]
        public int MatchId { get; set; }

        [Column("HomeGoals")]
        public int HomeGoals { get; set; }

        [Column("AwayGoals")]
        public int AwayGoals { get; set; }
    }
}
