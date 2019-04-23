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
    public class FavouriteController : Controller
    {
        private IRecipeRepository recipeRepository;
        private IFavouriteRepository favouriteRepository;
        private readonly SignInManager<User> _signInManager;

        public FavouriteController(IRecipeRepository recipe, IFavouriteRepository favourite, SignInManager<User> signInManager)
        {
            recipeRepository = recipe;
            favouriteRepository = favourite;
            _signInManager = signInManager;
        }

        public IActionResult Favourites()
        {
            return View();
        }

        public async Task<IActionResult> FavouriteRecipes()
        {
            try
            {
                User user = await _signInManager.UserManager.GetUserAsync(User);
                return PartialView("_FavouriteRecipesPartial", new FavouritesViewModel
                {
                    FavouriteRecipes = await recipeRepository.FindAllAsync(r => r.Favourites.Any(f => f.User == user))
                });
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public async Task<IActionResult> RemoveFromFavourite(Guid id)
        {
            Recipe recipe = await recipeRepository.FindAsync(r => r.Id == id);
            User user = await _signInManager.UserManager.GetUserAsync(User);
            Favourite favourite = recipe.Favourites.Where(f => f.User == user).FirstOrDefault();

            if (favourite != null)
            {
                favouriteRepository.Remove(favourite);
            }

            return PartialView("_FavouriteRecipesPartial", new FavouritesViewModel
            {
                FavouriteRecipes = await recipeRepository.FindAllAsync(r => r.Favourites.Any(f => f.User == user))
            });
        }
    }
}
