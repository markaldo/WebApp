using WebShop.MVC.Models;

namespace WebShop.MVC.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProduct();
    }
}
