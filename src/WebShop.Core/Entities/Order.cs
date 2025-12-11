using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        
        public string AdditionalInfo { get; set; }

        //One order can have many OrderLines
        //Each orderline represents a product in this order with a quantity
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        // sum of all line quantities * product price
        //public decimal TotalPrice => OrderLines.Sum(ol => ol.Quantity * ol.Product.Price);
        public decimal TotalPrice { get; set; }



    }
}
