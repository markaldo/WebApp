using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.MVC.Models;

namespace WebShop.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _repo.GetAllAsync();

            var model = categories.Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                IconUrl = c.IconUrl
            }).ToList();

            return View(model); // IEnumerable<CategoryViewModel>
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = new ProductCategory
            {
                CategoryName = model.CategoryName,
                IconUrl = model.IconUrl
            };

            await _repo.AddSync(category);
            await _repo.SaveChangeAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repo.GetById(id);
            if (category == null) return NotFound();

            var model = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IconUrl = category.IconUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = new ProductCategory
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                IconUrl = model.IconUrl
            };

            _repo.UpdateSync(category);
            await _repo.SaveChangeAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repo.GetById(id);
            if (category != null)
            {
                _repo.DeleteSync(category);
                await _repo.SaveChangeAsync();
            }

            return RedirectToAction("Index");
        }
    }

}
