using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Football match event
    /// </summary>
    [Table("FL_TB_Match")]
    public class Match
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("TeamHomeId")]
        public int TeamHomeId { get; set; }

        [Column("TeamAwayId")]
        public int TeamAwayId { get; set; }

        [Column("SeasonId")]
        public int SeasonId { get; set; }

        [Column("IsFinished")]
        public bool IsFinished { get; set; }

        [Column("StadiumId")]
        public int? StadiumId { get; set; }

        [Column("MatchDate")]
        public DateTime? Date { get; set; }

        public virtual Team TeamHome { get; set; }

        public virtual Team TeamAway { get; set; }

        public virtual Season Season { get; set; }

        public virtual Stadium Stadium { get; set; }

        public virtual MatchScore MatchScore { get; set; }

        public virtual IEnumerable<MatchMember> MatchMembers { get; set; }
    }
}
