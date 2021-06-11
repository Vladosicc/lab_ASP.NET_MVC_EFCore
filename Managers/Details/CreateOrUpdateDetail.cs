using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.Details
{
    public class CreateOrUpdateDetail
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Category> CategoryIdList { get; set; }
        public List<ModelCar> ModelCarIdList { get; set; }
        public List<BrandOfDetail> BrandOfDetailIdList { get; set; }
        public Guid Category { get; set; }
        public Guid ModelCar { get; set; }
        public Guid BrandOfDetail { get; set; }
    }
}
