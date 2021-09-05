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
    /// Address
    /// </summary>
    [Table("Address")]
    public class Address
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Postal { get; set; }

        public virtual AddressType AddressType { get; set; }
    }
}
