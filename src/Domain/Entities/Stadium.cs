using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Stadium
    /// </summary>
    [Table("FL_TB_Stadium")]
    public class Stadium
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("StadiumName")]
        public string Name { get; set; }

        [Column("NumberOfSeats")]
        public int NumberOfSeats { get; set; }

        [Column("AddressId")]
        public int AddressId { get; set; }

        [Column("Id")]
        public virtual Address Address { get; set; }
    }
}
