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


        public HomeController(ILogger<HomeController> logger, IProductService productServices)
        {
            _logger = logger;
            this._services = productServices;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _services.GetAllProduct();
            return View(model: products);
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
