using laba5_oop.Storage;
using laba5_oop.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.ModelCars
{
    public class ModelCarManager : IModelCarManager
    {
        private readonly AutoDataContext _dataContext;
        public ModelCarManager(AutoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ModelCar> AddModelCar(CreateOrUpdateModelCar createOrUpdateModelCar)
        {
            var entity = new ModelCar
            {
                Id = Guid.NewGuid(),
                Name = createOrUpdateModelCar.Name,
            };

            _dataContext.ModelCars.Add(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IReadOnlyCollection<ModelCar>> GetAll()
        {
            var query = _dataContext.ModelCars
                                    .OrderBy(d => d.Name)
                                    .AsNoTracking();

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<ModelCar> RemoveModelCar(ModelCar entity)
        {
            _dataContext.ModelCars.Remove(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<ModelCar> EditModelCar(ModelCar newEntity)
        {
            _dataContext.ModelCars.Update(newEntity);

            await _dataContext.SaveChangesAsync();

            return newEntity;
        }

        public async Task<ModelCar> FindModelCar(Guid id)
        {
            return await _dataContext.ModelCars.FindAsync(id);
        }
    }
}
