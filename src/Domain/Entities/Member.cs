using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Member of the league
    /// </summary>
    [Table("FL_TB_Member")]
    public class Member
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("MemberRoleId")]
        public int MemberRoleId { get; set; }

        [Column("TeamId")]
        public int TeamId { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        public virtual Team Team { get; set; }
        
        public virtual MemberRole MemberRole { get; set; }

        public virtual IEnumerable<MatchMember> MatchMembers { get; set; }
    }
}
