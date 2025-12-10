using WebShop.MVC.Models;

namespace WebShop.MVC.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategory();
    }
}
