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
    /// Player stat type
    /// </summary>
    [Table("FL_LG_PlayerStatType")]
    public class PlayerStatType
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("StatName")]
        public string StatName { get; set; }
    }
}
