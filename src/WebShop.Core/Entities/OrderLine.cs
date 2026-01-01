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
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }

        //Foreign key to Product - many orderlines can reference the same product
        public int ProductId { get; set; }
        public Product Product { get; set; }    

    }    
}
