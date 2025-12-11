using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Entities
{
    public class OrderLine
    {
        //Primary key for the OrderLine table
        public int Id { get; set; }

        //Foreign key to order many orderlines belong to one order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // subtotal 

        // Quantity of this product in the order
        public int Quantity { get; set; }

        //Foreign key to Product - many orderlines can reference the same product
        public int ProductId { get; set; }
        public Product Product { get; set; }    


    }
}
