using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Address
    /// </summary>
    [Table("FL_TB_Address")]
    public class Address
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("Street")]
        public string Street { get; set; }

        [Column("HouseNumber")]
        public string HouseNumber { get; set; }

        [Column("PostalCode")]
        public string PostalCode { get; set; }
    }
}
