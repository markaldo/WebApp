using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;    
namespace WebShop.Infra.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string DisplayName { get; set; }
        public bool IsVendor { get; set; }
    }
}
