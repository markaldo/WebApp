using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.MVC.Models;

namespace WebShop.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepo.GetAllAsync();
            var categories = await _categoryRepo.GetAllAsync();

            var categoryVMs = categories.Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();

            ViewBag.Categories = categoryVMs;

            var model = new List<ProductViewModel>();

            foreach (var p in products)
            {
                var badge = await _productRepo.GetBadgeAsync(p);

                model.Add(new ProductViewModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    SalePrice = p.SalePrice,
                    ImageUrl = p.ImageUrl,
                    Badge = badge,
                    CategoryId = p.ProductCategoryId
                });
            }

            return View(model);

        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var badge = await _productRepo.GetBadgeAsync(product);
            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                SalePrice = product.SalePrice,
                ImageUrl = product.ImageUrl,
                Badge = badge
            };
            return View(model: viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var product = new Product
            {
                ProductName = model.ProductName,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                ProductCategoryId = model.CategoryId
            };

            await _productRepo.AddSync(product);
            await _productRepo.SaveChangeAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var product = new Product
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                ProductCategoryId = model.CategoryId
            };

            _productRepo.UpdateSync(product);
            await _productRepo.SaveChangeAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepo.GetById(id);
            if (product != null)
            {
                _productRepo.DeleteSync(product);
                await _productRepo.SaveChangeAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
