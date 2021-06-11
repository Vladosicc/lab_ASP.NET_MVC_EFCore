using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Models
{
    public class ListBrandWithListDetail
    {
        public List<BrandOfDetail> BrandOfDetails { get; set; }
        public List<List<Detail>> Detail { get; set; }
    }
}
