using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Storage.Entity
{
    public class BrandOfDetail : IUniqIdEntity
    {
        [Key]
        [Required]
        [Column("IdBrand")]
        public Guid Id { get; set; }

        [Required]
        [Column("szName")]
        public string Name { get; set; }
    }
}
