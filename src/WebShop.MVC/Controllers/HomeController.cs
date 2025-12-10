using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShop.MVC.Interfaces;
using WebShop.MVC.Models;

namespace WebShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _services;
        private readonly ICategoryService _categoryservice;


        public HomeController(ILogger<HomeController> logger, IProductService productServices, ICategoryService categoryservice)
        {
            _logger = logger;
            this._services = productServices;
            this._categoryservice = categoryservice;
        }
        
        //public async Task<IActionResult> Index()
        //{

        //    var products = await _services.GetAllProduct();
        //    return View(model: products);
        //}

        public async Task<IActionResult> Index(int? CategoryId)
        {
            IEnumerable<ProductViewModel> products;

            var categories = await _categoryservice.GetAllCategory();
            if (CategoryId.HasValue)
            {
                products = await _services.GetAllProductByCategory(CategoryId);
            }
            else
            {
                products = await _services.GetAllProduct();
            }

            var viewModel = new HomeViewModel()
            {
                Products = products,
                Categories = categories
            };
            

            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
