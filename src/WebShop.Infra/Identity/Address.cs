using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Infra.Identity
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        public string LocationType { get; set; } = string.Empty; // "Home", "Work", etc.
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        public string AdditionalInfo {  get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
