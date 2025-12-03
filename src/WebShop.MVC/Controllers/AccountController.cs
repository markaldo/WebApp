using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        // GET: /Account/Index   -> Views/Account/Index.cshtml (page-account)
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Account/Login   -> Views/Account/Login.cshtml (page-login)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Login(/* LoginViewModel model */)
        /*{
            // TODO: validate credentials and sign user in
            // if (!ModelState.IsValid) return View(model);

            return RedirectToAction("Index", "Home");
        }*/

        // GET: /Account/Register   -> Views/Account/Register.cshtml (page-register)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Register(/* RegisterViewModel model */)
        /*{
            // TODO: create user account
            // if (!ModelState.IsValid) return View(model);

            return RedirectToAction("Login");
        }*/

        // GET: /Account/ForgotPassword   -> Views/Account/ForgotPassword.cshtml
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public IActionResult ForgotPassword(/* ForgotPasswordViewModel model */)
        /*{
            // TODO: send reset link
            return RedirectToAction("ForgotPasswordConfirmation");
        }*/

        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword   -> Views/Account/ResetPassword.cshtml
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            // pass token/email to view if needed
            return View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(/* ResetPasswordViewModel model */)
        {
            // TODO: reset password
            return RedirectToAction("Login");
        }

        // GET: /Account/PrivacyPolicy   -> Views/Account/PrivacyPolicy.cshtml
        [HttpGet]
        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        // GET: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // TODO: sign user out
            return RedirectToAction("Index", "Home");
        }
    }
}

