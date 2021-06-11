using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc.Async;
using laba5_oop.Managers.Details;
using laba5_oop.Managers.Orders;
using laba5_oop.Models;
using laba5_oop.Storage.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace laba5_oop.Controllers
{
    public class DetailController : Controller
    {
        readonly IDetailManager _detailManager;

        public DetailController (IDetailManager detailManager)
        {
            _detailManager = detailManager;
        }

        public async Task<ViewResult> ShowDetail()
        {
            var entity = await _detailManager.GetAll();

            return View(entity);
        }

        public ViewResult CreateDetail()
        {
            var TaskEntity = _detailManager.CreateOrUpdateDetail();
            var entity = TaskEntity.Result;

            ViewBag.CategoryIdList = new SelectList(entity.CategoryIdList, "Id","Name", entity.CategoryIdList.Select(it => it.Id));
            ViewBag.ModelCarIdList = new SelectList(entity.ModelCarIdList, "Id","Name", entity.ModelCarIdList.Select(it => it.Id));
            ViewBag.BrandIdList = new SelectList(entity.BrandOfDetailIdList, "Id", "Name", entity.BrandOfDetailIdList.Select(it => it.Id));
            return View();
        }

        public async Task<ViewResult> DetailsOfDetail(Detail entity)
        {
            entity = await _detailManager.FindId(entity.Id);
            var orderEntity = await _detailManager.GetAllOrders();
            ViewBag.OrderIdList = new SelectList(orderEntity, "Id", "Name", orderEntity.Select(it => it.Id));
            return View(entity);
        }

        public async Task<ViewResult> Edit(Detail oldEntity)
        {
            oldEntity = await _detailManager.FindId(oldEntity.Id);
            var entity = await _detailManager.CreateOrUpdateDetail();

            ViewBag.CategoryIdList = new SelectList(entity.CategoryIdList, "Id", "Name", entity.CategoryIdList.Select(it => it.Id));
            ViewBag.ModelIdList = new SelectList(entity.ModelCarIdList, "Id", "Name", entity.ModelCarIdList.Select(it => it.Id));
            ViewBag.BrandIdList = new SelectList(entity.BrandOfDetailIdList, "Id", "Name", entity.BrandOfDetailIdList.Select(it => it.Id));
            return View(oldEntity);
        }

        public ViewResult SearchResult(IEnumerable<Detail> entities)
        {
            return View(entities);
        }

        public async Task<ViewResult> SearchMenu()
        {
            var entity = await _detailManager.CreateOrUpdateDetail();
            entity.CategoryIdList.Add(new Category { Name = "" });
            entity.ModelCarIdList.Add(new ModelCar { Name = "" });
            entity.BrandOfDetailIdList.Add(new BrandOfDetail { Name = "" });

            ViewBag.CategoryIdList = new SelectList(entity.CategoryIdList, "Id", "Name", entity.CategoryIdList.Select(it => it.Id));
            ViewBag.ModelCarIdList = new SelectList(entity.ModelCarIdList, "Id", "Name", entity.ModelCarIdList.Select(it => it.Id));
            ViewBag.BrandIdList = new SelectList(entity.BrandOfDetailIdList, "Id", "Name", entity.BrandOfDetailIdList.Select(it => it.Id));
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateDetail createOrUpdateDetail)
        {
            await _detailManager.AddDetail(createOrUpdateDetail);
            return RedirectToAction(nameof(ShowDetail));
        }
        public async Task<ActionResult> Remove(Detail entity)
        {
            await _detailManager.RemoveDetail(entity);
            return RedirectToAction(nameof(ShowDetail));
        }

        public async Task<ActionResult> EditDetail(Detail oldEntity)
        {
            await _detailManager.EditDetail(oldEntity);
            return RedirectToAction(nameof(ShowDetail));
        }

        public async Task<ActionResult> Search(SearchModel searchModel)
        {
            var entity = await _detailManager.SearchFull(searchModel);
            return View(nameof(SearchResult), entity);
        }

        public async Task<ActionResult> SearchName(string Search)
        {
            var entities = await _detailManager.SearchName(Search);
            return View(nameof(SearchResult),entities);
        }
    }
}