using Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Storage.Entity
{
    public class Order : IUniqIdEntity
    {
        [Key]
        [Required]
        [Column("IdOrder")]
        public Guid Id { get; set; }

        [Required]
        [Column("szName")]
        public string Name { get; set; }

        [Required]
        [Column("decPrice")]
        public decimal Price { get; set; }

        [Required]
        [Column("szDetailsId")]
        public string DetailsId { get; set; }
    }
}
