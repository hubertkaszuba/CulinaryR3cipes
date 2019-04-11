using CulinaryR3cipes.Models;
using CulinaryR3cipes.Models.Interfaces;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ISendGridEmailSender _sendGridEmailSender;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ISendGridEmailSender sendGridEmailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _sendGridEmailSender = sendGridEmailSender;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if(!ModelState.IsValid)
                return View(login);

            var user = await _userManager.FindByEmailAsync(login.Email);

            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email lub nazwa użytkownika są niepoprawne");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if(ModelState.IsValid)
            {
                var user = new User { UserName = register.Name, Email = register.Email };
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    var emailVerifiactionCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _sendGridEmailSender.SendMail(emailVerifiactionCode);
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(register);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
