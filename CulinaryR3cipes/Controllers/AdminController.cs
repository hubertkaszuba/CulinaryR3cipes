using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryR3cipes.Models.ViewModels;
using CulinaryR3cipes.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using CulinaryR3cipes.Models;

namespace CulinaryR3cipes.Controllers
{
    public class AdminController : Controller
    {
        IRecipeRepository recipeRepository;
        private readonly SignInManager<User> _signInManager;

        public AdminController(IRecipeRepository recipe, SignInManager<User> signInManager)
        {
            recipeRepository = recipe;
            _signInManager = signInManager;
        }

        public IActionResult AdminPanel()
        {
            return View();
        }

        public async Task<IActionResult> RecipesToSubmit()
        {
            return PartialView("_RecipesToSubmitListPartial", new RecipesToSubmitListViewModel
            {
                RecipesToSubmit = await recipeRepository.FindAllAsync(r => r.IsSubmitted != true)
            });
        }

        public IActionResult Users()
        {
            return PartialView("_UsersListPartial", new UsersListViewModel
            {
                Users = _signInManager.UserManager.Users
            });
        }

        public async Task<IActionResult> Edit(int id)
        {

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {

            return View();
        }
    }
}
