using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.ModelCars
{
    public interface IModelCarManager
    {
        Task<IReadOnlyCollection<ModelCar>> GetAll();
        Task<ModelCar> AddModelCar(CreateOrUpdateModelCar createOrUpdateModelCar);
        Task<ModelCar> RemoveModelCar(ModelCar entity);
        Task<ModelCar> EditModelCar(ModelCar oldEntity);
        Task<ModelCar> FindModelCar(Guid id);
    }
}
