using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCIDentity.Models;
using MVCIDentity.Models.ViewModel;
using System.Data;

namespace MVCIDentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Identity> _siginManager;
        private readonly UserManager<Identity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<Identity> siginManager, UserManager<Identity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _siginManager = siginManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        public IActionResult Login()
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
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    Address = Model.Address
                };
                var result = await _userManager.CreateAsync(newUser, Model.Password);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByNameAsync(Model.UserName);

                if (User is not null)
                {
                    if (await _siginManager.CanSignInAsync(User))
                    {
                        var result = await _siginManager.PasswordSignInAsync(User, Model.Password, true, false);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _siginManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> EditUsers()
        {
            var lstRoles = _roleManager.Roles.ToList();
            var lstUsers =  _userManager.Users.ToList();    
            return View ((lstRoles, lstUsers));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsers(string? Roles, string? Users)
        {
            if (Roles is not null && Users is not null)
            {

                var User = await _userManager.FindByIdAsync(Users);

                if (User is not null)
                {
                    var currentRoles = (await _userManager.GetRolesAsync(User)).FirstOrDefault();

                    if (currentRoles is null) {

                        return await AddRoles(Roles, Users, User);

                    }
                    var RemovedRoles = await _userManager.RemoveFromRoleAsync(User, currentRoles);

                    if (RemovedRoles is not null && RemovedRoles.Succeeded)
                    {
                        return await AddRoles(Roles, Users, User);
                    }
                    else
                    {
                        ModelState.AddModelError("Summary", "Failed to remove User roles");
                    }
                }
                else
                {
                    return NotFound("User Not Found with the specific ID");
                }

            }






            ModelState.AddModelError("Summary", "either select lists are empty");

            var lstRoles = _roleManager.Roles.ToList();
            var lstUsers = _userManager.Users.ToList();
            return View((lstRoles, lstUsers));
        }

        private async Task<IActionResult> AddRoles(string Roles, string Users, Identity User)
        {
            var role =await _roleManager.FindByIdAsync(Roles);
            var addResult = await _userManager.AddToRoleAsync(User, role.Name);
            if (addResult.Succeeded)
            {
                await _siginManager.SignOutAsync();
                await _siginManager.SignInAsync(User,true);
                return RedirectToAction("index", "Home");
            }
            else
            {
                ModelState.AddModelError("Summary", "Failed to Add User roles");
                return NotFound("User Not Found with the specific ID");
            }
        }
    }
}
