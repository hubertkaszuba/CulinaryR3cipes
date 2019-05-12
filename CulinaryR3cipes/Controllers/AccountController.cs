using CulinaryR3cipes.Models;
using CulinaryR3cipes.Models.Interfaces;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
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

            if (user != null)
            {
                if (!_userManager.IsEmailConfirmedAsync(user).Result)
                {
                    ModelState.AddModelError("",
                    "Email not confirmed!");
                    return View(login);
                }

                if (user.isBanned)
                {
                    ModelState.AddModelError(string.Empty, "Użytkownik został zablokowany przez administratora");
                    return View(login);
                }

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
                if(_userManager.Users.Where(u => u.Email == register.Email).Any())
                {
                    ModelState.AddModelError("", "Istnieje już użytkownik z takim adresem email");
                    return View(register);
                }

                var user = new User { UserName = register.Name, Email = register.Email };
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    var emailVerifiactionCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        values: new { userId = user.Id, code = emailVerifiactionCode },
                        protocol: Request.Scheme);
                    await _sendGridEmailSender.SendMail(user.Email, "Potwierdź adres email", $"Potwierdź swoje konto, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klikając tutaj</a>.");
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Hasło powinno zawierać conajmniej 8 znaków, w tym: jedną dużą literę, jedną małą literę, cyfrę oraz znak specjalny");
                }
            }

            return View(register);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ConfirmEmail(string userid, string code)
        {
            User user = _userManager.FindByIdAsync(userid).Result;
            IdentityResult result = _userManager.
                        ConfirmEmailAsync(user, code).Result;
            if (result.Succeeded)
            {
                ViewBag.Message = "Twój adres email został potwierdzony!";
                return View();
            }
            else
            {
                ViewBag.Message = "Nastąpił błąd podczas potwierdzania Twojego adresu email!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordViewModel);

            User user =  _userManager.Users.Where(u => u.Email == forgotPasswordViewModel.Email).FirstOrDefault();

            if(user != null)
            {
                var passowordResetCode = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "Account",
                    values: new { userId = user.Id, code = passowordResetCode },
                    protocol: Request.Scheme);
                await _sendGridEmailSender.SendMail(user.Email, "Reset hasła", $"Aby zresetować hasło do swojego konta, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>kliknij tutaj</a>.");
            }

            return RedirectToAction("Login");
        }

        public IActionResult ResetPassword(string userid, string code)
        {
            return View(new ResetPasswordViewModel { Code = code } );
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordViewModel);

            User user = _userManager.Users.Where(u => u.Email == resetPasswordViewModel.Email).FirstOrDefault();

            if(user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                    return View();
            }
            else
                return View();
        }
    }
}
