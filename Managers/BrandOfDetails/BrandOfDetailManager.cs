using laba5_oop.Models;
using laba5_oop.Storage;
using laba5_oop.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.BrandOfDetails
{
    public class BrandOfDetailManager : IBrandOfDetailManager
    {
        private readonly AutoDataContext _dataContext;

        public BrandOfDetailManager(AutoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BrandOfDetail> AddBrandOfDetail(BrandOfDetail createOrUpdateBrandOfDetail)
        {
            var entity = new BrandOfDetail
            {
                Id = Guid.NewGuid(),
                Name = createOrUpdateBrandOfDetail.Name,
            };

            _dataContext.BrandOfDetails.Add(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<ListBrandWithListDetail> GetAll()
        {
            var entity = new ListBrandWithListDetail() {Detail = new List<List<Detail>>() };

            var allDetails = await _dataContext.Details
                                    .OrderBy(d => d.Name)
                                    .Include(d => d.Category)
                                    .Include(d => d.ModelCar)
                                    .ToListAsync();

            entity.BrandOfDetails = await _dataContext.BrandOfDetails
                                    .OrderBy(d => d.Name)
                                    .ToListAsync();

            for (int i = 0; i < entity.BrandOfDetails.Count(); i++)
            {
                entity.Detail.Add(allDetails.Where(d => d.BrandId == entity.BrandOfDetails[i].Id).ToList());
            }

            return entity;
        }

        public async Task<BrandOfDetail> RemoveBrandOfDetail(BrandOfDetail entity)
        {
            _dataContext.BrandOfDetails.Remove(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<BrandOfDetail> EditBrandOfDetail(BrandOfDetail oldEntity)
        {
            _dataContext.BrandOfDetails.Update(oldEntity);

            await _dataContext.SaveChangesAsync();

            return oldEntity;
        }

        public async Task<BrandOfDetail> FindBrandOfDetail(Guid id)
        {
            return await _dataContext.BrandOfDetails.FindAsync(id);
        }
    }
}
