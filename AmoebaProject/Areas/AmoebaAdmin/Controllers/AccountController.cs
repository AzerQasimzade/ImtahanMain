using AmoebaProject.Areas.ViewModels;
using AmoebaProject.Models;
using AmoebaProject.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaProject.Areas.AmoebaAdmin.Controllers
{
    [Area("AmoebaAdmin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.Username,
                Surname = registerVM.Surname,
            };
            IdentityResult identityResult=await _userManager.CreateAsync(user,registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                    return View(error);
                }
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();    
            return RedirectToAction("Index", "Home",new {area=""});
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser existed = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if (existed == null)
            {
                existed = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
                if (existed == null)
                {
                    ModelState.AddModelError(String.Empty, "Username,Email or Password is incorrect");
                    return View();
                }
            }
            var passResult=await _signInManager.PasswordSignInAsync(existed, loginVM.Password,loginVM.IsRemembered,false);
            if (!passResult.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Username,Email or Password is incorrect");
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = item.ToString(),
                });
            }
            return RedirectToAction("Index", "Home", new { area = "" }); ;
        }
    }
}
