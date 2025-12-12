using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Entities
{
    public class CartItem
    {
        public required Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => Product.Price * Quantity;
    }
}
