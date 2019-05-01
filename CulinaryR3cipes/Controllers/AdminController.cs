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
        IRatingRepository ratingRepository;
        private readonly SignInManager<User> _signInManager;
        private const int _reportsLimit = 5;

        public AdminController(IRecipeRepository recipe, IRatingRepository rating, SignInManager<User> signInManager)
        {
            recipeRepository = recipe;
            ratingRepository = rating;
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

        public async Task<IActionResult> Users()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            return PartialView("_UsersListPartial", new UsersListViewModel
            {
                Users = _signInManager.UserManager.Users.Where(u => !u.isBanned && u.Id != user.Id)
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            recipeRepository.Remove(await recipeRepository.FindAsync(x => x.Id == id));
            return PartialView("_RecipesToSubmitListPartial", new RecipesToSubmitListViewModel
            {
                RecipesToSubmit = await recipeRepository.FindAllAsync(r => r.IsSubmitted != true)
            });
        }

        public async Task<IActionResult> RecipeDetails(Guid id)
        {
            Recipe recipe = await recipeRepository.FindAsync(r => r.Id == id);
            return PartialView("_RecipeToSubmitDetails", recipe);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRecipe(Recipe recipe)
        {
            Recipe recipeToSubmitt = await recipeRepository.FindAsync(r => r.Id == recipe.Id);
            recipeToSubmitt.IsSubmitted = true;
            recipeRepository.Update(recipeToSubmitt);
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
        public async Task<IActionResult> BanUser(string id)
        {
            User user = _signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user != null)
            {
                user.isBanned = true;
                await _signInManager.UserManager.UpdateAsync(user);
            }
            
            return PartialView("_UsersListPartial", new UsersListViewModel
            {
                Users = _signInManager.UserManager.Users
            });
        }

        [HttpPost]
        public async Task<IActionResult> BanUserFromAlerts(string id)
        {
            User user = _signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user != null)
            {
                var recipes = await recipeRepository.FindAllAsync(r => r.Ratings.All(ra => ra.User == user));
                
                foreach (var recipe in recipes)
                {
                    recipe.Ratings.Remove(recipe.Ratings.Where(r => r.User == user).First());
                    recipe.AverageRating = recipe.Ratings.Any() ? recipe.Ratings.Average(r => r.RatingValue) : 0;
                    recipeRepository.Update(recipe);
                }
                    
                user.isBanned = true;
                await _signInManager.UserManager.UpdateAsync(user);
            }

            return PartialView("_AlertsListPartial", new UsersListViewModel
            {
                Users = _signInManager.UserManager.Users.Where(u => !u.isBanned)
            });
        }

        public async Task<IActionResult> Alerts()
        {
            return PartialView("_AlertsListPartial", new AlertsListViewModel
            {
                Ratings = await ratingRepository.FindAllAsync(rating => rating.ReportsCounter >= _reportsLimit)
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            Rating ratingToDelete = await ratingRepository.FindAsync(r => r.Id == id);

            Recipe recipeToUpdate = ratingToDelete.Recipe;

            ratingRepository.Remove(ratingToDelete);

            recipeToUpdate.AverageRating = recipeToUpdate.Ratings.Any() ? recipeToUpdate.Ratings.Average(r => r.RatingValue) : 0;
            recipeRepository.Update(recipeToUpdate);

            return PartialView("_AlertsListPartial", new AlertsListViewModel
            {
                Ratings = await ratingRepository.FindAllAsync(rating => rating.ReportsCounter >= _reportsLimit)
            });
        }

        [HttpPost]
        public async Task<IActionResult> SetAdminRole(User user)
        {
            User Admin = _signInManager.UserManager.Users.Where(u => u.Id == user.Id).FirstOrDefault();

            if (Admin != null)
            {
                bool isInRole = await _signInManager.UserManager.IsInRoleAsync(Admin, "Admin");

                if(isInRole)
                    await _signInManager.UserManager.RemoveFromRoleAsync(Admin, "Admin");
                else
                    await _signInManager.UserManager.AddToRoleAsync(Admin, "Admin");
            }
                
            return PartialView("_UsersListPartial", new UsersListViewModel
            {
                Users = _signInManager.UserManager.Users
            });
        }
    }
}
