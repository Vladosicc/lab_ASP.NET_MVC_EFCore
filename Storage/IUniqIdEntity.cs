using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Storage
{
    public interface IUniqIdEntity
    {
        [Key]
        [Required]
        [Column("IdDetail")]
        public Guid Id { get; set; }
    }
}
