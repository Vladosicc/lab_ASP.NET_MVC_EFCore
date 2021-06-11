using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba5_oop.Models;
using laba5_oop.Storage.Entity;

namespace laba5_oop.Managers.Details
{
    public interface IDetailManager
    {
        Task<IEnumerable<Detail>> GetAll();
        Task<Detail> AddDetail(CreateOrUpdateDetail createOrUpdateDetail);
        Task<CreateOrUpdateDetail> CreateOrUpdateDetail();
        Task<Detail> RemoveDetail(Detail entity);
        Task<IReadOnlyCollection<Order>> GetAllOrders();
        Task<Detail> FindId(Guid guid);
        Task<Detail> EditDetail(Detail detail);
        Task<IEnumerable<Detail>> SearchName(string name);
        Task<IEnumerable<Detail>> SearchFull(SearchModel searchModel);
    }
}
