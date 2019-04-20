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
                .Where(recipe => recipe.IsSubmitted)
                .OrderBy(r => r.AverageRating)
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

        public async Task<IActionResult> Recipes(Guid [] SelectedTypeIds, Guid[] IncludedCategoryIds, Guid [] ExcludedCategoryIds, string name, int recipePage = 1)
        {
            IEnumerable<Recipe> filteredRecipes = await recipeRepository.FindAllAsync(r => (SelectedTypeIds.Contains(r.Type.Id) || SelectedTypeIds.Count() == 0)
                && (r.Ingredients.Any(x => IncludedCategoryIds.Any(y => y == x.Product.Category.Id)) || IncludedCategoryIds.Count() == 0)
                && (!r.Ingredients.Any(x => ExcludedCategoryIds.Any(y => y == x.Product.Category.Id)) || ExcludedCategoryIds.Count() == 0)
                && (name == null || r.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));

            RecipesListViewModel viewModel = new RecipesListViewModel
            {
                Recipes = filteredRecipes
                .Where(recipe => recipe.IsSubmitted)
                .OrderBy(r => r.AverageRating)
                .Skip((recipePage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = recipePage,
                    ItemsPerPage = PageSize,
                    TotalItems = filteredRecipes.Where(recipe => recipe.IsSubmitted).Count()
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
                IEnumerable<Recipe> filteredRecipes = await recipeRepository.FindAllAsync(r => r.Ingredients.All(i => productsInFridge.Any(p => (p.Id == i.Id && (p.Quantity >= i.Quantity)))));
          
                RecipesListViewModel viewModel = new RecipesListViewModel
                {
                    Recipes = filteredRecipes.OrderBy(r => r.AverageRating)
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

        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            Recipe recipe = await recipeRepository.FindAsync(r => r.Id == id);            
            return PartialView("_RecipeDetails", new RecipeDetailsViewModel { Recipe = recipe,
                DidUserRate = recipe.Ratings.Any(rating => rating.User == user),
                AverageRate = (int)Math.Floor(recipe.AverageRating) });
        }

        public async Task<IActionResult> SetRating(RecipeDetailsViewModel recipeDetailsViewModel)
        {
            Recipe recipe = await recipeRepository.FindAsync(r => r.Id == recipeDetailsViewModel.Recipe.Id);
            var user = await _signInManager.UserManager.GetUserAsync(User);
            Rating rating = new Rating
            {
                Recipe = recipe,
                Comment = recipeDetailsViewModel.Rating.Comment,
                RatingValue = recipeDetailsViewModel.Rating.RatingValue,
                User = user
            };
            recipe.Ratings.Add(rating);
            recipe.AverageRating = recipe.Ratings.Average(r => r.RatingValue);
            recipeRepository.Update(recipe);

            return PartialView("_RecipeDetails", new RecipeDetailsViewModel { Recipe = await recipeRepository.FindAsync(r => r.Id == recipeDetailsViewModel.Recipe.Id), DidUserRate = true });
        }
        
        public async Task <IActionResult> Comments (Guid id, int commentPage = 1)
        {
            Recipe recipe = await recipeRepository.FindAsync(r => r.Id == id);
            return PartialView("_Comments", new CommentsViewModel { RecipeId = id, Ratings = recipe.Ratings.OrderBy(rating => rating.Id).Skip((commentPage-1)*CommentsPageSize).Take(CommentsPageSize).ToList(), PagingInfo = new PagingInfo { CurrentPage = commentPage, ItemsPerPage = CommentsPageSize, TotalItems = recipe.Ratings.Count } });
        }

        public async Task<IActionResult> ReportComment(Guid id, int commentPage, Guid reportId)
        {
            Recipe recipe = await recipeRepository.FindAsync(r => r.Id == id);
            recipe.Ratings.Where(rating => rating.Id == reportId).First().ReportsCounter++;

            recipeRepository.Update(recipe);
            return PartialView("_Comments", new CommentsViewModel { RecipeId = id, Ratings = recipe.Ratings.OrderBy(rating => rating.Id).Skip((commentPage - 1) * CommentsPageSize).Take(CommentsPageSize).ToList(), PagingInfo = new PagingInfo { CurrentPage = commentPage, ItemsPerPage = CommentsPageSize, TotalItems = recipe.Ratings.Count }, ReportedCommentId = reportId });
        }
    }
}