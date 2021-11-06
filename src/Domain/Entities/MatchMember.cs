using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("FL_TB_MatchMember")]
    public class MatchMember
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("MatchId")]
        public int MatchId { get; set; }

        [Column("MemberId")]
        public int MemberId { get; set; }

        [Column("IsMemberInHomeTeam")]
        public bool IsMemberInHomeTeam { get; set; }

        public virtual Match Match { get; set; }

        public virtual Member Member { get; set; }
    }
}
