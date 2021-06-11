using laba5_oop.Models;
using laba5_oop.Storage;
using laba5_oop.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace laba5_oop.Managers.Details
{
    public class DetailManager : IDetailManager
    {
        private readonly AutoDataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DetailManager(AutoDataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Detail> AddDetail(CreateOrUpdateDetail createOrUpdateDetail)
        {
            var entity = new Detail
            {
                Id = Guid.NewGuid(),
                Name = createOrUpdateDetail.Name,
                CategoryId = createOrUpdateDetail.Category,
                ModelId = createOrUpdateDetail.ModelCar,
                BrandId = createOrUpdateDetail.BrandOfDetail,
                Price = createOrUpdateDetail.Price
            };

            _dataContext.Details.Add(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Detail>> GetAll()
        {
            var query = _dataContext.Details
                                    .Include(d => d.Category)
                                    .Include(d => d.ModelCar)
                                    .Include(d => d.Brand)
                                    .OrderBy(d => d.Category.Name)
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

        public async Task<CreateOrUpdateDetail> CreateOrUpdateDetail()
        {
            return new CreateOrUpdateDetail
            {
                CategoryIdList = await _dataContext.Categories.ToListAsync(),
                ModelCarIdList = await _dataContext.ModelCars.ToListAsync(),
                BrandOfDetailIdList = await _dataContext.BrandOfDetails.ToListAsync()
            };
        }

        public async Task<Detail> RemoveDetail(Detail entity)
        {
            _dataContext.Details.Remove(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Detail> FindId(Guid guid)
        {
            var entity = await _dataContext.Details.FindAsync(guid);
            var category = await _dataContext.Categories.FindAsync(entity.CategoryId);
            var modelcar = await _dataContext.ModelCars.FindAsync(entity.ModelId);
            var brand = await _dataContext.BrandOfDetails.FindAsync(entity.BrandId);
            entity.Category = category;
            entity.ModelCar = modelcar;
            entity.Brand = brand;
            return entity;
        }

        public async Task<Detail> EditDetail(Detail detail)
        {
            _dataContext.Details.Update(detail);

            await _dataContext.SaveChangesAsync();

            return detail;
        }

        public async Task<IEnumerable<Detail>> SearchName(string name)
        {
            var query = _dataContext.Details.Where(en => en.Name.ToLower() == name.ToLower())
                                           .Include(d => d.Category)
                                           .Include(d => d.ModelCar)
                                           .Include(d => d.Brand)
                                           .AsNoTracking();

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<Detail>> SearchFull(SearchModel searchModel)
        {
            var entities = await GetAll();

            if(searchModel.Name != null && entities.Count() != 0)
            {
                entities = entities.Where(en => en.Name.ToLower() == searchModel.Name.ToLower());
            }
            if (searchModel.Price != null && entities.Count() != 0)
            {
                decimal price;
                try
                {
                    price = decimal.Parse(searchModel.Price);
                }
                catch
                {
                    return new List<Detail>();
                }
                entities = entities.Where(en => en.Price == price);
            }
            if (searchModel.CategoryId != Guid.Empty && entities.Count() != 0)
            {
                entities = entities.Where(en => en.CategoryId == searchModel.CategoryId);
            }
            if (searchModel.ModelCarId != Guid.Empty && entities.Count() != 0)
            {
                entities = entities.Where(en => en.ModelId == searchModel.ModelCarId);
            }
            if (searchModel.BrandId != Guid.Empty && entities.Count() != 0)
            {
                entities = entities.Where(en => en.ModelId == searchModel.ModelCarId);
            }

            return entities;
        }
    }
}
