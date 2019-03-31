using CulinaryR3cipes.Models;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Controllers
{
    public class RecipeController : Controller
    {
        IProductRepository productRepository;

        public RecipeController(IProductRepository product)
        {
            productRepository = product;
        }

        public async Task<IActionResult> Recipe()
        {

            return View(new RecipeViewModel
            {
                Recipe = new Models.Recipe(),
                Products = await productRepository.Products()
            });
        }
    }
}
