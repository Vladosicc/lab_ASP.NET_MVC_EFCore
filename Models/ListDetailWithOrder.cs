using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Models
{
    public class ListDetailWithOrder
    {
        public List<Detail> Details { get; set; }
        public Guid OrderId { get; set; }
        public Guid DetailGuid { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
