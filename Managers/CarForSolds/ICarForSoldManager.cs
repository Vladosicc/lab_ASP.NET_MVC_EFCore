using laba5_oop.Models;
using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.CarForSolds
{
    public interface ICarForSoldManager
    {
        Task<IEnumerable<CarForSold>> GetAll();
        Task<CreateOrUpdateCarForSold> CreateOrUpdateCarForSold();
        Task<CarForSold> AddCarForSold(CreateOrUpdateCarForSold createOrUpdateCarForSold);
        Task<CarForSold> FindId(Guid guid);
        Task<CarForSold> EditCarForSold(CarForSold CarForSold);
        Task<IEnumerable<CarForSold>> SearchName(string name);
        Task<IEnumerable<CarForSold>> SearchFull(SearchModel searchModel);
        Task<CarForSold> RemoveCarForSold(CarForSold entity);
    }
}
