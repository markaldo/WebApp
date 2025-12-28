using System.ComponentModel.DataAnnotations;

namespace WebShop.MVC.Models
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public bool IsVendor { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}