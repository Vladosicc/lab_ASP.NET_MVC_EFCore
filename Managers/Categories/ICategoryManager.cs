using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.Categories
{
    public interface ICategoryManager
    {
        Task<IReadOnlyCollection<Category>> GetAll();
        Task<Category> AddCategory(CreateOrUpdateCategory createOrUpdateDetail);
        Task<Category> RemoveCategory(Category entity);
        Task<Category> EditCategory(Category oldEntity);
        Task<Category> FindCategory(Guid id);
    }
}
