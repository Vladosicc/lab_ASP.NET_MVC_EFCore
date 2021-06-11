using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Storage.Entity 
{
    [Table("tblDetail")]
    public class Detail : IUniqIdEntity
    {
        [Key]
        [Required]
        [Column("IdDetail")]
        public Guid Id { get; set; }

        [Required]
        [Column("szName")]
        public string Name { get; set; }

        [Required]
        [Column("decPrice")]
        public decimal Price { get; set; }

        [Required]
        [Column("IdCategory")]
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Required]
        [Column("IdCarModel")]
        public Guid ModelId { get; set; }
        [ForeignKey(nameof(ModelId))]
        public ModelCar ModelCar { get; set; }

        [Required]
        [Column("IdBrand")]
        public Guid BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public BrandOfDetail Brand { get; set; }
    }
}
