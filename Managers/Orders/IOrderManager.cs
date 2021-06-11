using laba5_oop.Models;
using laba5_oop.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace laba5_oop.Managers.Orders
{
    public interface IOrderManager
    {
        Task<Order> AddOrder(Detail detail);
        Task<IReadOnlyCollection<Order>> GetAll();
        Task<Order> CreateOrder(CreateOrUpdateOrder orUpdateOrder);
        Task<Order> RemoveOrder(Order entity);
        Task<Order> FindId(Guid id);
        Task<Order> EditOrder(Order order);
        Task<ListDetailWithOrder> EntityForDetails(Order order);
        Task<AsyncVoidMethodBuilder> DeleteFromOrder(Guid deleteId, Guid fromId, decimal deletePrice);
    }
}
