using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryR3cipes.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CulinaryR3cipes.Models.ViewModels;

namespace CulinaryR3cipes.Models
{
    public class HomeController : Controller
    {
        private IRecipeRepository repository;
        int PageSize = 2;

        public HomeController(IRecipeRepository repo, IProductRepository repo2)
        {
            repository = repo;
        }

        public ViewResult Index(int recipePage = 1) => View(new RecipesListViewModel
        {
            Recipes = repository.Recipes
                .OrderBy(r => r.RecipeId)
                .Skip((recipePage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = recipePage,
                ItemsPerPage = PageSize,
                TotalItems = repository.Recipes.Count()
            }
        });
    }
}