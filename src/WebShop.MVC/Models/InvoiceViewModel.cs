using WebShop.Core.Entities;
using WebShop.Infra.Identity;

namespace WebShop.MVC.Models
{
    public class InvoiceViewModel
    {
        public Order Order { get; set; } = null!;
        public Address? Address { get; set; }
        public ApplicationUser? User { get; set; }
        public GuestAddressDto? GuestAddress { get; set; } 
    }

}
