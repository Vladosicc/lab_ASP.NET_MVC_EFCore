using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba5_oop.Managers.Categories;
using laba5_oop.Storage.Entity;
using Microsoft.AspNetCore.Mvc;

namespace laba5_oop.Controllers
{
    public class CategoryController : Controller
    {
        readonly ICategoryManager _categoryManager;

        public CategoryController (ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<ViewResult> ShowCategory()
        {
            var entity = await _categoryManager.GetAll();

            return View(entity);
        }
         
        public ViewResult AddCategory()
        {
            return View();
        }

        public async Task<ViewResult> EditCategory(Category oldEntity)
        {
            var entity = await _categoryManager.FindCategory(oldEntity.Id);
            return View(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Add(CreateOrUpdateCategory createOrUpdateCategory)
        {
            await _categoryManager.AddCategory(createOrUpdateCategory);
            return RedirectToAction(nameof(ShowCategory));
        }

        public async Task<ActionResult> Remove(Category entity)
        {
            await _categoryManager.RemoveCategory(entity);
            return RedirectToAction(nameof(ShowCategory));
        }

        public async Task<ActionResult> Edit(Category oldEntity)
        {
            await _categoryManager.EditCategory(oldEntity);
            return RedirectToAction(nameof(ShowCategory));
        }
            
    }
}