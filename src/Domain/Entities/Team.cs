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
    /// Football team
    /// </summary>
    [Table("FL_TB_Team")]
    public class Team
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("TeamName")]
        public string Name { get; set; }

        [Column("AddressId")]
        public int? AddressId { get; set; }

        [Column("StadiumId")]
        public int? StadiumId { get; set; }

        public virtual Address Address{ get; set; }
        public virtual Stadium Stadium { get; set; }
    }
}
