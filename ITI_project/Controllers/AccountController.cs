using ITI_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITI_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly Context context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Context context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }


        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = context.Roles.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM userModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userModel.UserName;
                user.PasswordHash = userModel.Password;
                user.Address = userModel.Address;
                                                                            //to make it hashed
                IdentityResult result = await userManager.CreateAsync(user, userModel.Password);

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userModel.Roles);

                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            ViewBag.Roles = context.Roles.ToList();
            return View("Register", userModel);
        }


        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM userModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userModel.UserName);

                if(user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userModel.Password);
                    if (found)
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("UserAddress", user.Address));

                        await signInManager.SignInWithClaimsAsync(user, userModel.RememberMe, claims);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User Name or Password is wrong");
            }
            return View("Login", userModel);
        }
    }
}
