using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Infra.DependencyInjection;
using WebShop.Infra.Identity;
using WebShop.Infra.Services;
using WebShop.MVC.Models; // your view models (RegisterViewModel, LoginViewModel, ChangePasswordViewModel, ResetPasswordViewModel)

namespace WebShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAddress _addressService;

        public AccountController(
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAddress addressService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _addressService = addressService;
        }

        // GET: /Account/Account
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Account()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login");

            var userId = user.Id;
            var userAddresses = await _addressService.GetUserAddressesAsync(userId);
            var defaultAddress = userAddresses.FirstOrDefault(a => a.IsDefault);

            var model = new AccountOverviewViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DisplayName = user.DisplayName,
                Email = user.Email,

                AddressLine1 = defaultAddress?.AddressLine1 ?? "",
                AddressLine2 = defaultAddress?.AddressLine2 ?? "",
                City = defaultAddress?.City ?? "",
                PostalCode = defaultAddress?.PostalCode ?? "",
                Country = defaultAddress?.Country ?? "",
                Phone = defaultAddress?.AdditionalInfo ?? user.PhoneNumber ?? "",
                HasSavedAddress = defaultAddress != null
            };

            return View(model);
        }

        // POST: /Account/Account
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Account(AccountOverviewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DisplayName = model.DisplayName;

            if (!string.IsNullOrEmpty(model.AddressLine1))
            {
                var userId = user.Id;
                var userAddresses = await _addressService.GetUserAddressesAsync(userId);
                var defaultAddress = userAddresses.FirstOrDefault(a => a.IsDefault);

                var address = defaultAddress ?? new Address
                {
                    UserId = userId,
                    LocationType = "Home"
                };

                address.AddressLine1 = model.AddressLine1;
                address.AddressLine2 = model.AddressLine2;
                address.City = model.City;
                address.PostalCode = model.PostalCode;
                address.Country = model.Country;
                address.AdditionalInfo = model.Phone; 

                if (defaultAddress == null)
                {
                    address.IsDefault = true;
                    await _addressService.SaveAddressAsync(address);
                }
                else
                {
                    await _addressService.UpdateAddressAsync(address);
                }
            }

            if (user.Email != model.Email)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);

            TempData["Message"] = "Account updated successfully.";
            return RedirectToAction(nameof(Account));
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
                return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Index", "Home");

            if (result.IsLockedOut)
                ModelState.AddModelError("", "Account locked. Try again later.");
            else
                ModelState.AddModelError("", "Invalid login attempt.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginCheckout(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid login data" });

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
                return Json(new { success = true, message = "Login successful!" });

            if (result.IsLockedOut)
                return Json(new { success = false, message = "Account locked. Try again later." });

            return Json(new { success = false, message = "Invalid login attempt." });
        }

        //Post: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Console.WriteLine("The email is : " + model.Email);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                DisplayName = model.DisplayName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.IsVendor)
                    await _userManager.AddToRoleAsync(user, "Vendor");
                else
                    await _userManager.AddToRoleAsync(user, "Customer");

                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // GET: /Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword() => View();

        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            // Validate username/email
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                // Show validation error instead of redirecting
                ModelState.AddModelError("Email", "User does not exist.");
                return View(model);
            }

            // No token needed — go directly to ResetPassword
            return RedirectToAction("Reset", new ResetPasswordViewModel { Email = user.Email });

        }

        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation() => View();

        // GET: /Account/ResetPassword
        [HttpGet]
        public IActionResult Reset(string email)
        {
            var model = new ResetPasswordViewModel { Email = email };
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(ResetPasswordViewModel model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            var remove = await _userManager.RemovePasswordAsync(user);
            if (!remove.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove old password.");
                return View(model);
            }

            // Add new password
            var add = await _userManager.AddPasswordAsync(user, model.Password);
            if (!add.Succeeded)
            {
                foreach (var error in add.Errors)
                    ModelState.AddModelError("Invalid", error.Description);

                return View(model);
            }
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        // GET: /Account/PrivacyPolicy
        [HttpGet]
        public IActionResult Privacy() => RedirectToAction("Privacy", "Home");



        // GET: /Account/ChangePassword
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword() => View();

        // POST: /Account/ChangePassword
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Message"] = "Password changed successfully.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
    }
}