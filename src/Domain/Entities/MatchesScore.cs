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
    [Table("FL_VW_MatchesScore")]
    public class MatchesScore
    {
        [Key]
        [Column("MatchId")]
        public int MatchId { get; set; }

        [Column("HomeTeamId")]
        public int HomeTeamId { get; set; }

        [Column("AwayTeamId")]
        public int AwayTeamId { get; set; }

        [Column("HomeGoals")]
        public int HomeGoals { get; set; }

        [Column("AwayGoals")]
        public int AwayGoals { get; set; }

        [Column("SeasonId")]
        public int SeasonId { get; set; }

        public virtual Match Match { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual Team AwayTeam { get; set; }

        public virtual Season Season { get; set; }
    }
}
