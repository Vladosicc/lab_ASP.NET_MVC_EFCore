using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Storage.Entity
{
    public class CarForSold : IUniqIdEntity
    {
        [Key]
        [Required]
        [Column("IdCar")]
        public Guid Id { get; set; }

        [Required]
        [Column("szName")]
        public string Name { get; set; }

        [Required]
        [Column("intMileage")]
        public int Mileage { get; set; }
        
        [Required]
        [Column("decPrice")]
        public decimal Price { get; set; }

        [Required]
        [Column("IdCarModel")]
        public Guid ModelId { get; set; }
        [ForeignKey(nameof(ModelId))]
        public ModelCar ModelCar { get; set; }
    }
}
