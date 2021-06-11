using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Models
{
    public class SearchModel
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Mileage { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ModelCarId { get; set; }
        public Guid BrandId { get; set; }
    }
}
