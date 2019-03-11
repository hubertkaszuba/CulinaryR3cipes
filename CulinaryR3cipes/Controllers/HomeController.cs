using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryR3cipes.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryR3cipes.Models
{
    public class HomeController : Controller
    {
        private IRecipeRepository repository;

        public HomeController(IRecipeRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            var x = repository.Recipes;
            return View("Index");
        }
    }
}