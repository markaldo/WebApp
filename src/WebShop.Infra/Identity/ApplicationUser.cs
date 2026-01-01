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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Phone { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public bool IsVendor { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
