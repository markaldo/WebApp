using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Repositories;
using WebShop.MVC.Models;

namespace WebShop.Core.Components
{
    public class CartHeaderViewComponent : ViewComponent
    {
        private readonly IShoppingCartService _cartService;

        public CartHeaderViewComponent(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _cartService.GetCartItemsAsync();
            var total = await _cartService.GetTotalAsync();

            var vm = new CartHeaderViewModel
            {
                ItemCount = items.Sum(i => i.Quantity),
                Total = total,
                Items = items.Select(i => new CartHeaderItemViewModel
                {
                    ProductId = i.Product.Id,
                    ProductName = i.Product.ProductName,
                    ImageUrl = string.IsNullOrEmpty(i.Product.ImageUrl)
                        ? "/assets/imgs/shop/thumbnail-1.jpg"
                        : i.Product.ImageUrl,
                    Price = i.Product.Price,
                    Quantity = i.Quantity
                })
            };

            return View(vm);
        }
    }

}
