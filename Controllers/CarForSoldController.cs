using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba5_oop.Managers.CarForSolds;
using laba5_oop.Models;
using laba5_oop.Storage.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace laba5_oop.Controllers
{
    public class CarForSoldController : Controller
    {
        readonly ICarForSoldManager _carForSoldManager;

        public CarForSoldController(ICarForSoldManager CarForSoldManager)
        {
            _carForSoldManager = CarForSoldManager;
        }
        public async Task<ViewResult> Show()
        {
            var entity = await _carForSoldManager.GetAll();

            return View(entity);
        }
        public ViewResult Create()
        {
            var TaskEntity = _carForSoldManager.CreateOrUpdateCarForSold();
            var entity = TaskEntity.Result;

            ViewBag.ModelCarIdList = new SelectList(entity.ModelCarIdList, "Id", "Name", entity.ModelCarIdList.Select(it => it.Id));
            return View();
        }
        public async Task<ViewResult> Edit(CarForSold oldEntity)
        {
            oldEntity = await _carForSoldManager.FindId(oldEntity.Id);
            var entity = await _carForSoldManager.CreateOrUpdateCarForSold();

            ViewBag.ModelIdList = new SelectList(entity.ModelCarIdList, "Id", "Name", entity.ModelCarIdList.Select(it => it.Id));
            return View(oldEntity);
        }

        public ViewResult SearchResult(IEnumerable<CarForSold> entities)
        {
            return View(entities);
        }

        public async Task<ViewResult> Detail(CarForSold entity)
        {
            entity = await _carForSoldManager.FindId(entity.Id);
            return View(entity);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCar(CreateOrUpdateCarForSold createOrUpdateCarForSold)
        {
            await _carForSoldManager.AddCarForSold(createOrUpdateCarForSold);
            return RedirectToAction(nameof(Show));
        }
        public async Task<ActionResult> EditCar(CarForSold oldEntity)
        {
            await _carForSoldManager.EditCarForSold(oldEntity);
            return RedirectToAction(nameof(Show));
        }
        public async Task<ActionResult> Search(SearchModel searchModel)
        {
            var entity = await _carForSoldManager.SearchFull(searchModel);
            return View(nameof(SearchResult), entity);
        }

        public async Task<ActionResult> SearchName(string Search)
        {
            var entities = await _carForSoldManager.SearchName(Search);
            return View(nameof(SearchResult), entities);
        }

        public async Task<ActionResult> Delete(CarForSold entity)
        {
            await _carForSoldManager.RemoveCarForSold(entity);
            return RedirectToAction(nameof(Show));
        }
    }
}