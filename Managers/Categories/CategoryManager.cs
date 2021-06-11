using laba5_oop.Storage;
using laba5_oop.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.Categories
{
    public class CategoryManager : ICategoryManager
    {
        private readonly AutoDataContext _dataContext;

        public CategoryManager(AutoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Category> AddCategory(CreateOrUpdateCategory createOrUpdateDetail)
        {
            var entity = new Category
            {
                Id = Guid.NewGuid(),
                Name = createOrUpdateDetail.Name,
            };

            _dataContext.Categories.Add(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IReadOnlyCollection<Category>> GetAll()
        {
            var query = _dataContext.Categories
                                    .OrderBy(d => d.Name)
                                    .AsNoTracking();

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<Category> RemoveCategory(Category entity)
        {
            _dataContext.Categories.Remove(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Category> EditCategory(Category oldEntity)
        {
            _dataContext.Categories.Update(oldEntity);

            await _dataContext.SaveChangesAsync();

            return oldEntity;
        }

        public async Task<Category> FindCategory(Guid id)
        { 
            return await _dataContext.Categories.FindAsync(id);
        }
    }
}
