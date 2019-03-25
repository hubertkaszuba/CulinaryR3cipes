using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryR3cipes.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CulinaryR3cipes.Models
{
    public class HomeController : Controller
    {
        private IRecipeRepository recipeRepository;
        private ITypeRepository typeRepository;
        private ICategoryRepository categoryRepository;
        private IIngredientRepository ingredientRepository;
        int PageSize = 2;

        public HomeController(IRecipeRepository recipe, ITypeRepository type, ICategoryRepository category, IIngredientRepository ingredient)
        {
            recipeRepository = recipe;
            typeRepository = type;
            categoryRepository = category;
            ingredientRepository = ingredient;
        }

        public ViewResult Index(int recipePage = 1)
        {
            RecipesListViewModel viewModel = new RecipesListViewModel
            {
                Recipes = recipeRepository.Recipes
                .OrderBy(r => r.RecipeId)
                .Skip((recipePage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = recipePage,
                    ItemsPerPage = PageSize,
                    TotalItems = recipeRepository.Recipes.Count()
                },
                Types = typeRepository.Types,
                Categories = categoryRepository.Categories                
            };
        
            return View(viewModel);
        }

        //[HttpPost]
        public IActionResult Recipes(string [] SelectedTypeIds, string[] IncludedCategoryIds, string [] ExcludedCategoryIds, string name, int recipePage = 1)
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

            List<Ingredient> ingredientsIncluded = ingredientRepository.Ingredients.Where(i => includedCategoryFilter.Contains(i.Product.CategoryId.ToString())).ToList();
            List<Ingredient> ingredientsExcluded = ingredientRepository.Ingredients.Where(i => excludedCategoryFilter.Contains(i.Product.CategoryId.ToString())).ToList();

            IEnumerable<Recipe> recipes = recipeRepository.Recipes
                .Where(r => (typesFilter.Contains(r.TypeId.ToString()) || typesFilter.Count == 0)
                && (r.Ingredients.Any(x => ingredientsIncluded.Any(y => y.IngredientId == x.IngredientId)) || includedCategoryFilter.Count == 0)
                && (!r.Ingredients.Any(x => ingredientsExcluded.Any(y => y.IngredientId == x.IngredientId)) || excludedCategoryFilter.Count == 0)
                && (r.Name.Contains(name) || name == null));

            RecipesListViewModel viewModel = new RecipesListViewModel
            {
                Recipes = recipes.OrderBy(r => r.RecipeId)
                .Skip((recipePage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = recipePage,
                    ItemsPerPage = PageSize,
                    TotalItems = recipes.Count()
                },
            };

            return PartialView("_RecipesListPartial", viewModel);
        }
    }
}