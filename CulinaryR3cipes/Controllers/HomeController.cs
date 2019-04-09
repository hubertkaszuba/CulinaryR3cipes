using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryR3cipes.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using CulinaryR3cipes.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace CulinaryR3cipes.Models
{
    public class HomeController : Controller
    {
        private IRecipeRepository recipeRepository;
        private ITypeRepository typeRepository;
        private ICategoryRepository categoryRepository;
        private IIngredientRepository ingredientRepository;
        private IFridgeRepository fridgeRepository;
        private readonly SignInManager<User> _signInManager;
        int PageSize = 1;
        int CommentsPageSize = 2;

        public HomeController(IRecipeRepository recipe, ITypeRepository type, ICategoryRepository category, IIngredientRepository ingredient, IFridgeRepository fridge, SignInManager<User> signInManager)
        {
            recipeRepository = recipe;
            typeRepository = type;
            categoryRepository = category;
            ingredientRepository = ingredient;
            fridgeRepository = fridge;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(int recipePage = 1)
        {
            IEnumerable<Recipe> recipes = await recipeRepository.Recipes();
            RecipesListViewModel viewModel = new RecipesListViewModel
            {
                Recipes = recipes
                .OrderBy(r => r.RecipeId)
                .Skip((recipePage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = recipePage,
                    ItemsPerPage = PageSize,
                    TotalItems = recipes.Count()
                },
                Types = await typeRepository.Types(),
                Categories = await categoryRepository.Categories()                
            };
        
            return View(viewModel);
        }

        //[HttpPost]
        public async Task<IActionResult> Recipes(string [] SelectedTypeIds, string[] IncludedCategoryIds, string [] ExcludedCategoryIds, string name, int recipePage = 1)
        {
            List<string> typesFilter = new List<string> { };
            if (SelectedTypeIds.Any())
            {
                foreach (var s in SelectedTypeIds)
                    typesFilter.Add(s);
            }

            List<string> includedCategoryFilter = new List<string> { };
            if (IncludedCategoryIds.Any())
            {
                foreach (var s in IncludedCategoryIds)
                    includedCategoryFilter.Add(s);
            }

            List<string> excludedCategoryFilter = new List<string> { };
            if (ExcludedCategoryIds.Any())
            {
                foreach (var s in ExcludedCategoryIds)
                    excludedCategoryFilter.Add(s);
            }

            IEnumerable<Ingredient> ingredientsIncluded = await ingredientRepository.FindAllAsync(i => includedCategoryFilter.Contains(i.Product.CategoryId.ToString()));
            IEnumerable<Ingredient> ingredientsExcluded = await ingredientRepository.FindAllAsync(i => excludedCategoryFilter.Contains(i.Product.CategoryId.ToString()));

            IEnumerable<Recipe> filteredRecipes = await recipeRepository.FindAllAsync(r => (typesFilter.Contains(r.TypeId.ToString()) || typesFilter.Count == 0)
                && (r.Ingredients.Any(x => ingredientsIncluded.Any(y => y.IngredientId == x.IngredientId)) || includedCategoryFilter.Count == 0)
                && (!r.Ingredients.Any(x => ingredientsExcluded.Any(y => y.IngredientId == x.IngredientId)) || excludedCategoryFilter.Count == 0)
                && (name == null || r.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));

            RecipesListViewModel viewModel = new RecipesListViewModel
            {
                Recipes = filteredRecipes.OrderBy(r => r.RecipeId)
                .Skip((recipePage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = recipePage,
                    ItemsPerPage = PageSize,
                    TotalItems = filteredRecipes.Count()
                },
            };

            return PartialView("_RecipesListPartial", viewModel);
        }

        public async Task<IActionResult> RecipesByFridge(int recipePage = 1)
        {
            try
            {
                User user = await _signInManager.UserManager.GetUserAsync(User);
                IEnumerable<Fridge> productsInFridge = await fridgeRepository.GetUserProducts(user);
                IEnumerable<Recipe> filteredRecipes = await recipeRepository.FindAllAsync(r => r.Ingredients.All(i => productsInFridge.Any(p => (p.ProductId == i.ProductId && (p.Quantity >= i.Quantity)))));
          
                RecipesListViewModel viewModel = new RecipesListViewModel
                {
                    Recipes = filteredRecipes.OrderBy(r => r.RecipeId)
                    .Skip((recipePage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = recipePage,
                        ItemsPerPage = PageSize,
                        TotalItems = filteredRecipes.Count()
                    }
                };
                return PartialView("_RecipesListPartial", viewModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            Recipe recipe = await recipeRepository.FindAsync(r => r.RecipeId == id);            
            return PartialView("_RecipeDetails", new RecipeDetailsViewModel { Recipe = recipe,
                DidUserRate = recipe.Ratings.Any(rating => rating.User == user),
                AverageRate = recipe.Ratings.Any() ? (int)Math.Floor(recipe.Ratings.Average(ratings => ratings.RatingValue)) : 0});
        }

        public async Task<IActionResult> SetRating(RecipeDetailsViewModel recipeDetailsViewModel)
        {
            Recipe recipe = await recipeRepository.FindAsync(r => r.RecipeId == recipeDetailsViewModel.Recipe.RecipeId);
            var user = await _signInManager.UserManager.GetUserAsync(User);
            Rating rating = new Rating
            {
                Recipe = recipe,
                Comment = recipeDetailsViewModel.Rating.Comment,
                RatingValue = recipeDetailsViewModel.Rating.RatingValue,
                User = user
            };
            recipe.Ratings.Add(rating);
            recipeRepository.UpdateRecipe(recipe);

            return PartialView("_RecipeDetails", new RecipeDetailsViewModel { Recipe = await recipeRepository.FindAsync(r => r.RecipeId == recipeDetailsViewModel.Recipe.RecipeId), DidUserRate = true });
        }

        public async Task <IActionResult> Comments (int id, int commentPage = 1)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            Recipe recipe = await recipeRepository.FindAsync(r => r.RecipeId == id);
            return PartialView("_Comments", new CommentsViewModel { RecipeId = id, Ratings = recipe.Ratings.OrderBy(rating => rating.RatingId).Skip((commentPage-1)*CommentsPageSize).Take(CommentsPageSize).ToList(), PagingInfo = new PagingInfo { CurrentPage = commentPage, ItemsPerPage = CommentsPageSize, TotalItems = recipe.Ratings.Count } });
        }
    }
}