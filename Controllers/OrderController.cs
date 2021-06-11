using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba5_oop.Managers.Orders;
using laba5_oop.Models;
using laba5_oop.Storage.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace laba5_oop.Controllers
{
    public class OrderController : Controller
    {
        readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public async Task<ViewResult> ShowOrder()
        {
            var entity = await _orderManager.GetAll();

            return View(entity);
        }

        public ViewResult CreateOrder()
        {
            return View();
        }

        public async Task<ViewResult> Edit(Order oldEntity)
        {
            oldEntity = await _orderManager.FindId(oldEntity.Id);

            return View(oldEntity);
        }
        public async Task<ViewResult> Details(Order order)
        {
            var entity = await _orderManager.EntityForDetails(order);
            return View(entity);
        }

        [HttpPost]
        public async Task<ActionResult> InOrderFromDetail(Detail entity)
        {
            await _orderManager.AddOrder(entity);

            return RedirectToAction("ShowDetail", "Detail");
        }

        public async Task<ActionResult> Create(CreateOrUpdateOrder orUpdateOrder)
        {
            await _orderManager.CreateOrder(orUpdateOrder);
            return RedirectToAction("ShowDetail", "Detail");
        }

        public async Task<ActionResult> Remove(Order entity)
        {
            await _orderManager.RemoveOrder(entity);
            return RedirectToAction(nameof(ShowOrder));
        }

        public async Task<ActionResult> EditOrder(Order oldEntity)
        {
            await _orderManager.EditOrder(oldEntity);
            return RedirectToAction(nameof(ShowOrder));
        }

        public async Task<ActionResult> DeleteFromOrder(Guid id, Guid OrderId, string price)
        {
            decimal Price = decimal.Parse(price);
            await _orderManager.DeleteFromOrder(id, OrderId, Price);
            return RedirectToAction(nameof(ShowOrder));
        }
    }
}