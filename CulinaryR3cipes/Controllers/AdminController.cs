using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryR3cipes.Models.ViewModels;
using CulinaryR3cipes.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using CulinaryR3cipes.Models;
using Microsoft.AspNetCore.Authorization;

namespace CulinaryR3cipes.Controllers
{
    [Authorize(Roles = ("Admin"))]
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

        [HttpPost]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            recipeRepository.DeleteRecipe(await recipeRepository.FindAsync(x => x.RecipeId == id));
            return PartialView("_RecipesToSubmitListPartial", new RecipesToSubmitListViewModel
            {
                RecipesToSubmit = await recipeRepository.FindAllAsync(r => r.IsSubmitted != true)
            });
        }

        public async Task<IActionResult> RecipeDetails(int id)
        {
            Recipe recipe = await recipeRepository.FindAsync(r => r.RecipeId == id);
            return PartialView("_RecipeToSubmitDetails", recipe);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRecipe(Recipe recipe)
        {
            Recipe recipeToSubmitt = await recipeRepository.FindAsync(r => r.RecipeId == recipe.RecipeId);
            recipeToSubmitt.IsSubmitted = true;
            recipeRepository.UpdateRecipe(recipeToSubmitt);
            return PartialView("_RecipesToSubmitListPartial", new RecipesToSubmitListViewModel
            {
                RecipesToSubmit = await recipeRepository.FindAllAsync(r => r.IsSubmitted != true)
            });
        }

        public IActionResult UserDetails(string id)
        {
            User user = _signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault();
            return PartialView("_UserDetails", user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _signInManager.UserManager.DeleteAsync(_signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault());

            return PartialView("_UsersListPartial", new UsersListViewModel
            {
                Users = _signInManager.UserManager.Users
            });
        }

        [HttpPost]
        public async Task<IActionResult> SetAdminRole(User user)
        {
            User newAdmin = _signInManager.UserManager.Users.Where(u => u.Id == user.Id).FirstOrDefault();

            if (newAdmin != null)
                await _signInManager.UserManager.AddToRoleAsync(newAdmin, "Admin");

            return PartialView("_UsersListPartial", new UsersListViewModel
            {
                Users = _signInManager.UserManager.Users
            });
        }
    }
}
