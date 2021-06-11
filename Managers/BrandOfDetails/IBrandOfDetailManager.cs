using laba5_oop.Models;
using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laba5_oop.Managers.BrandOfDetails
{
    public interface IBrandOfDetailManager
    {
        Task<ListBrandWithListDetail> GetAll();
        Task<BrandOfDetail> AddBrandOfDetail(BrandOfDetail createOrUpdateBrandOfDetail);
        Task<BrandOfDetail> FindBrandOfDetail(Guid id);
        Task<BrandOfDetail> EditBrandOfDetail(BrandOfDetail oldEntity);
        Task<BrandOfDetail> RemoveBrandOfDetail(BrandOfDetail entity);
    }
}
