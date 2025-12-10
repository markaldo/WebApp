using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.MVC.Interfaces;
using WebShop.MVC.Models;

namespace WebShop.MVC.Services
{
    public class CategoryServices : ICategoryService
    {   

        private readonly ICategoryRepository categoryRepository;
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategory()
        {   
            var categories = await categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryViewModel{
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });
               


            
        }
    }
}
