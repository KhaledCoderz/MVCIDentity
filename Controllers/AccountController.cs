using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCIDentity.Models;
using MVCIDentity.Models.ViewModel;

namespace MVCIDentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _siginManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> siginManager, UserManager<IdentityUser> userManager)
        {
            _siginManager = siginManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Identity
                {
                    UserName = Model.UserName,
                    Email = Model.Email,
                };
                var result = await _userManager.CreateAsync(newUser, Model.Password);
            }
            return View();
        }
    }
}
