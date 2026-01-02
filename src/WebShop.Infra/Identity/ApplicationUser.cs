using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebShop.Core.Entities;
namespace WebShop.Infra.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string DisplayName { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsVendor { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
