using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba5_oop.Managers.BrandOfDetails;
using laba5_oop.Storage.Entity;
using Microsoft.AspNetCore.Mvc;

namespace laba5_oop.Controllers
{
    public class BrandOfDetailController : Controller
    {
        readonly IBrandOfDetailManager _brandOfDetailManager;

        public BrandOfDetailController(IBrandOfDetailManager brandOfDetailManager)
        {
            _brandOfDetailManager = brandOfDetailManager;
        }

        public async Task<ViewResult> Show()
        {
            var entity = await _brandOfDetailManager.GetAll();

            return View(entity);
        }

        public ViewResult Create()
        {
            return View();
        }

        public async Task<ViewResult> Edit(BrandOfDetail oldEntity)
        {
            var entity = await _brandOfDetailManager.FindBrandOfDetail(oldEntity.Id);
            return View(entity);
        }

        [HttpPost]

        public async Task<ActionResult> CreateBrand(BrandOfDetail brandOfDetailController)
        {
            await _brandOfDetailManager.AddBrandOfDetail(brandOfDetailController);

            return RedirectToAction(nameof(Show));
        }

        public async Task<ActionResult> EditBrand(BrandOfDetail oldEntity)
        {
            await _brandOfDetailManager.EditBrandOfDetail(oldEntity);
            return RedirectToAction(nameof(Show));
        }

        public async Task<ActionResult> Remove(BrandOfDetail entity)
        {
            await _brandOfDetailManager.RemoveBrandOfDetail(entity);
            return RedirectToAction(nameof(Show));
        }
    }
}