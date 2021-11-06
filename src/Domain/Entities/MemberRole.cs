using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Member role
    /// </summary>
    [Table("FL_TB_MemberRole")]
    public class MemberRole
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("RoleName")]
        public string Name { get; set; }

        [Column("IsPlayer")]
        public bool IsPlayer { get; set; }
        public virtual IEnumerable<Member> Members { get; set; }
    }
}
