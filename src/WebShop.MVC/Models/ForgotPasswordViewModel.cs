using System.ComponentModel.DataAnnotations;

namespace WebShop.MVC.Models
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}