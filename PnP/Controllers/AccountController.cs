using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PnP.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace PnP.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private UserManager<IdentityUser> userManager;
    private SignInManager<IdentityUser> signInManager;


    public AccountController(UserManager<IdentityUser> UserMgr,
      SignInManager<IdentityUser> signInMgr)
    {
        userManager = UserMgr;
        signInManager = signInMgr;
    }
    [AllowAnonymous]
    public ViewResult Login(string returnUrl)
    {
      return View(new LoginModel
      {
        ReturnUrl = returnUrl
      });
    }
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
      if (ModelState.IsValid)
      {
        IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);

        if(user != null)
        {
          await signInManager.SignOutAsync();
          if ((await signInManager.PasswordSignInAsync(user,
            loginModel.Password, false, false)).Succeeded)
          {
            return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
          }

        }
      }
      ModelState.AddModelError("", "Invalid name or password");
      return View(loginModel);
    }


    public async Task<RedirectResult> Logout(string returnUrl = "/")
    {
      await signInManager.SignOutAsync();
      return Redirect(returnUrl);
    }
  }
}