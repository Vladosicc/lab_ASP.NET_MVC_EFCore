using laba5_oop.Storage;
using laba5_oop.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba5_oop.Models;
using System.Runtime.CompilerServices;

namespace laba5_oop.Managers.Orders
{
    public class OrderManager : IOrderManager
    {
        private readonly AutoDataContext _dataContext;

        public OrderManager (AutoDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        // Сделать во всплывающем окне VievBag и заполнить его из Orders.Name
        // Добавлять в этот заказ
        public async Task<Order> AddOrder(Detail detail)
        {
            bool flag = false;
            var priceDetail = await _dataContext.Details.FindAsync(detail.Id);
            var order = await _dataContext.Orders.FindAsync(detail.CategoryId);
            if(order == null)
            {
                order = new Order { Id = Guid.NewGuid(), DetailsId = "", Name = "empty", Price = 0 };
                flag = true;
            }
            order.Price += priceDetail.Price;
            if (order.DetailsId == "")
            {
                var Guids = new List<Guid>() { detail.Id };
                order.DetailsId = JsonSerializer.Serialize(Guids);
            }
            else
            {
                var Guids = JsonSerializer.Deserialize<List<Guid>>(order.DetailsId);
                Guids.Add(detail.Id);
                order.DetailsId = JsonSerializer.Serialize(Guids);
            }
            if(flag)
            {
                _dataContext.Orders.Add(order);
            }
            else
            {
                _dataContext.Orders.Update(order);
            }

            await _dataContext.SaveChangesAsync();

            return order;
        }

        public async Task<IReadOnlyCollection<Order>> GetAll()
        {
            var query = _dataContext.Orders.OrderBy(ent => ent.Name)
                                           .AsNoTracking();
            
            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<Order> CreateOrder(CreateOrUpdateOrder orUpdateOrder)
        {
            var entity = new Order
            {
                Name = orUpdateOrder.Name,
                Id = Guid.NewGuid(),
                DetailsId = "",
                Price = 0
            };

            _dataContext.Orders.Add(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Order> RemoveOrder(Order entity)
        {
            _dataContext.Orders.Remove(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Order> FindId(Guid id)
        {
            return await _dataContext.Orders.FindAsync(id);
        }

        public async Task<Order> EditOrder(Order order)
        {
            _dataContext.Orders.Update(order);

            await _dataContext.SaveChangesAsync();

            return order;
        }

        public async Task<ListDetailWithOrder> EntityForDetails(Order order)
        {
            bool FlagVerify = false;
            decimal PriceVerify = 0;
            List<Guid> Guids;
            List<Guid> oldGuids = new List<Guid>();
            order = await FindId(order.Id);
            var entity = new ListDetailWithOrder() { OrderId = order.Id, Name = order.Name, Price = order.Price, Details = new List<Detail>() };
            if (order.DetailsId == "")
            {
                Guids = null;
            }
            else
            {
                Guids = JsonSerializer.Deserialize<List<Guid>>(order.DetailsId);
            }
            if (Guids != null)
            {
                foreach(var item in Guids)
                {
                    Detail detail = await _dataContext.Details.FindAsync(item);
                    if (detail != null)
                    {
                        PriceVerify += detail.Price;
                        entity.Details.Add(detail);
                    }
                    else
                    {
                        FlagVerify = true;
                        oldGuids.Add(item);
                    }
                }
                foreach(var item in oldGuids)
                {
                    Guids.Remove(item);
                }
            }
            if(FlagVerify)
            {
                order.Price = PriceVerify;
                order.DetailsId = JsonSerializer.Serialize(Guids);
                _dataContext.Orders.Update(order);

                await _dataContext.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<AsyncVoidMethodBuilder> DeleteFromOrder(Guid deleteId, Guid fromId, decimal deletePrice)
        {
            var entity = await _dataContext.Orders.FindAsync(fromId);
            var Guids = JsonSerializer.Deserialize<List<Guid>>(entity.DetailsId);
            Guids.Remove(deleteId);
            entity.DetailsId = JsonSerializer.Serialize(Guids);
            entity.Price -= deletePrice;
            _dataContext.Orders.Update(entity);

            await _dataContext.SaveChangesAsync();

            return new AsyncVoidMethodBuilder();
        }
    }
}
