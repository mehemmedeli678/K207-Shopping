using K207Shopping.Data;
using K207Shopping.Models;
using K207Shopping.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<k207User> _userManager;
        private readonly SignInManager<k207User> _signInManager;
        private readonly ShoppingContext _context;

        public AuthController(ShoppingContext context, SignInManager<k207User> signInManager, UserManager<k207User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  LogIn(LogInVM logInVM)
        {
            if (!ModelState.IsValid)
            {
                return View(logInVM);
            }
            k207User appUser = await _userManager.FindByEmailAsync(logInVM.Email);
            if (appUser == null)
            {   
                ModelState.AddModelError("","Email yalnisdir");
                return View(logInVM);
            }
           Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, logInVM.password, logInVM.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Sifre yalnisdir");
                return View(logInVM);
            }
            return RedirectToAction("Index","home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM RegisterVM)
        {
            if (!ModelState.IsValid)
            {
                return View(RegisterVM);
            }
            k207User newUser = new k207User()
            {
                UserName=RegisterVM.UserName,
                Email = RegisterVM.Email,
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, RegisterVM.password);
            if (result.Succeeded)
            {
                IdentityResult res = await _userManager.AddToRoleAsync(newUser, "User");
               return RedirectToAction(nameof(LogIn));
            }
            return View(RegisterVM);
        }

    }
}
