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
    private RoleManager<IdentityRole> roleManager;


    public AccountController(UserManager<IdentityUser> UserMgr,
      SignInManager<IdentityUser> signInMgr, RoleManager<IdentityRole> roleMgr)
    {
      userManager = UserMgr;
      signInManager = signInMgr;
      roleManager = roleMgr;
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

            if (loginModel.Name == "Admin")
            {
              if (!roleManager.RoleExistsAsync("Admin").Result)
              {
                IdentityRole role = new IdentityRole
                {
                  Name = "Admin"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                if (!roleResult.Succeeded)
                {
                  ModelState.AddModelError("", "Error while creating role");
                  return View(loginModel);
                }
              }
              userManager.AddToRoleAsync(user, "Admin").Wait();

              return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
            }
            else
              return Redirect(loginModel?.ReturnUrl ?? "/");
          }

        }
      }
      ModelState.AddModelError("", "Invalid Login");
      return View(loginModel);
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult Register(RegisterModel registerModel)
    {
      if (ModelState.IsValid)
      {
        IdentityUser user = new IdentityUser
        {
          UserName = registerModel.Name,
          Email = registerModel.Email
        };


        IdentityResult result = userManager.CreateAsync(user, registerModel.Password).Result;

        if (result.Succeeded)
        {
          if (!roleManager.RoleExistsAsync("NormalUser").Result)
          {
            IdentityRole role = new IdentityRole
            {
              Name = "NormalUser"
            };
            IdentityResult roleResult = roleManager.CreateAsync(role).Result;

            if (!roleResult.Succeeded)
            {
              ModelState.AddModelError("", "Error while creating role");
              return View(registerModel);
            }
          }
          userManager.AddToRoleAsync(user, "NormalUser");
          return RedirectToAction("Login", "Account");
        }
      }
      return View(registerModel);
    }


    public async Task<RedirectResult> Logout(string returnUrl = "/")
    {
      await signInManager.SignOutAsync();
      return Redirect(returnUrl);
    }
  }
}
