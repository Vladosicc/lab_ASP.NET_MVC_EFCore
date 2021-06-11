using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba5_oop.Managers.ModelCars;
using laba5_oop.Storage.Entity;
using Microsoft.AspNetCore.Mvc;

namespace laba5_oop.Controllers
{
    public class ModelCarController : Controller
    {
        readonly IModelCarManager _modelCarManager;

        public ModelCarController(IModelCarManager categoryModelCar)
        {
            _modelCarManager = categoryModelCar;
        }

        [HttpGet]
        public async Task<ViewResult> ShowModelCar()
        {
            var entity = await _modelCarManager.GetAll();

            return View(entity);
        }

        public ViewResult AddModelCar()
        {
            return View();
        }

        public async Task<ViewResult> EditModelCar(ModelCar oldEntity)
        {
            var entity = await _modelCarManager.FindModelCar(oldEntity.Id);

            return View(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Add(CreateOrUpdateModelCar createOrUpdateCategory)
        {
            await _modelCarManager.AddModelCar(createOrUpdateCategory);
            return RedirectToAction(nameof(ShowModelCar));
        }

        public async Task<ActionResult> Remove(ModelCar entity)
        {
            await _modelCarManager.RemoveModelCar(entity);
            return RedirectToAction(nameof(ShowModelCar));
        }

        public async Task<ActionResult> Edit(ModelCar oldEntity)
        {
            await _modelCarManager.EditModelCar(oldEntity);
            return RedirectToAction(nameof(ShowModelCar));
        }
    }
}