using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.CarForSolds
{
    public class CreateOrUpdateCarForSold
    {
        public string Name { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public List<ModelCar> ModelCarIdList { get; set; }
        public Guid ModelId { get; set; }
    }
}
