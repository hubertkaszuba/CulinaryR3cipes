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
        int PageSize = 2;

        public HomeController(IRecipeRepository recipe, ITypeRepository type)
        {
            recipeRepository = recipe;
            typeRepository = type;
        }

        public ViewResult Index(int recipePage = 1)
        {
            ViewBag.Types = typeRepository.Types.Select(r => new SelectListItem { Value = r.TypeId.ToString(), Text = r.Name }).ToList();
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
                Types = typeRepository.Types
            };
        
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Recipes(string [] SelectedTypeIds, int recipePage = 1)
        {
            List<string> typesFilter = new List<string> { };
            if (SelectedTypeIds.Any())
            {
                foreach (var s in SelectedTypeIds)
                    typesFilter.Add(s);
            }

            //var categoryIncludedFilter = recipesViewModel.SelectedIncludedCategoriesIds.Split(",").ToList();
            //var categoryExcludedFilter = recipesViewModel.SelectedExcludedCategoriesIds.Split(",").ToList();

            RecipesListViewModel viewModel = new RecipesListViewModel
            {
                Recipes = recipeRepository.Recipes
                .Where(r => typesFilter.Contains(r.TypeId.ToString()) || typesFilter.Count == 0)
                .OrderBy(r => r.RecipeId)
                .Skip((recipePage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = recipePage,
                    ItemsPerPage = PageSize,
                    TotalItems = recipeRepository.Recipes.Count()
                },
            };

            return PartialView("_RecipesListPartial", viewModel);
        }
    }
}