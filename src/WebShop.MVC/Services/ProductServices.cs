using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.MVC.Interfaces;
using WebShop.MVC.Models;

namespace WebShop.MVC.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProduct()
        {
            var product = await productRepository.GetAllAsync();
            var productViewModel = product.Select(p => new ProductViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category.CatergoryName
            }).ToList();
            return productViewModel;
        }
    }
}
