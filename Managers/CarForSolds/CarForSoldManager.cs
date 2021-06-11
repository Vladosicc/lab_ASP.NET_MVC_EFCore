using laba5_oop.Models;
using laba5_oop.Storage;
using laba5_oop.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.CarForSolds
{
    public class CarForSoldManager : ICarForSoldManager
    {
        private readonly AutoDataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarForSoldManager(AutoDataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CarForSold> AddCarForSold(CreateOrUpdateCarForSold createOrUpdateCarForSold)
        {
            var entity = new CarForSold
            {
                Id = Guid.NewGuid(),
                Name = createOrUpdateCarForSold.Name,
                Mileage = createOrUpdateCarForSold.Mileage,
                ModelId = createOrUpdateCarForSold.ModelId,
                Price = createOrUpdateCarForSold.Price
            };

            _dataContext.CarForSolds.Add(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<CarForSold>> GetAll()
        {
            var query = _dataContext.CarForSolds
                                    .Include(d => d.ModelCar)
                                    .OrderBy(d => d.Name)
                                    .AsNoTracking();

            var entities = await query.ToListAsync();

            return entities;
        }
        public async Task<IReadOnlyCollection<Order>> GetAllOrders()
        {
            var query = _dataContext.Orders.OrderBy(ent => ent.Name)
                                           .AsNoTracking();

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<CreateOrUpdateCarForSold> CreateOrUpdateCarForSold()
        {
            return new CreateOrUpdateCarForSold
            {
                ModelCarIdList = await _dataContext.ModelCars.ToListAsync()
            };
        }

        public async Task<CarForSold> RemoveCarForSold(CarForSold entity)
        {
            _dataContext.CarForSolds.Remove(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<CarForSold> FindId(Guid guid)
        {
            var entity = await _dataContext.CarForSolds.FindAsync(guid);
            var modelcar = await _dataContext.ModelCars.FindAsync(entity.ModelId);
            entity.ModelCar = modelcar;
            return entity;
        }

        public async Task<CarForSold> EditCarForSold(CarForSold CarForSold)
        {
            _dataContext.CarForSolds.Update(CarForSold);

            await _dataContext.SaveChangesAsync();

            return CarForSold;
        }

        public async Task<IEnumerable<CarForSold>> SearchName(string name)
        {
            var query = _dataContext.CarForSolds.Where(en => en.Name.ToLower() == name.ToLower())
                                           .AsNoTracking();

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<CarForSold>> SearchFull(SearchModel searchModel)
        {
            var entities = await GetAll();

            if (searchModel.Name != null && entities.Count() != 0)
            {
                entities = entities.Where(en => en.Name.ToLower() == searchModel.Name.ToLower());
            }
            if (searchModel.Price != null && entities.Count() != 0)
            {
                int milleage;
                try
                {
                    milleage = int.Parse(searchModel.Mileage);
                }
                catch
                {
                    return new List<CarForSold>();
                }
                entities = entities.Where(en => en.Price == milleage);
            }
            if (searchModel.ModelCarId != Guid.Empty && entities.Count() != 0)
            {
                entities = entities.Where(en => en.ModelId == searchModel.ModelCarId);
            }

            return entities;
        }
    }
}
