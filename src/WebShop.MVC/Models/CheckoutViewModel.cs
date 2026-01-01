using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebShop.MVC.Models
{
    public class CheckoutViewModel
    {
        // Billing/Shipping Information
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; } = string.Empty;

        [Display(Name = "Address Line 2")]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal code is required")]
        [Display(Name = "Postal Code / ZIP")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string Country { get; set; } = "Poland";
        public string? AdditionalInfo { get; set; } = string.Empty;

        // Guest vs Logged-in user handling
        public bool IsGuest { get; set; } = true;

        // Address management 
        public List<SelectListItem> SavedAddresses { get; set; } = new();
        public int? SelectedAddressId { get; set; }
        public bool SaveAddress { get; set; }
        public string AddressName { get; set; } = "Home"; // "Work", "Home", etc.

        // Order summary (populated by controller)
        public decimal OrderTotal { get; set; }
        public int ItemCount { get; set; }
    }

}

