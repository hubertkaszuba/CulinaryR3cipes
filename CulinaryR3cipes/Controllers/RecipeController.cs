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

        public IActionResult Recipe2()
        {
            return View();
        }

        public IActionResult Recipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Recipe (RecipeViewModel model)
        {

            return RedirectToAction("Recipe");
        }

        public async Task<JsonResult> GetData()
        {
            IEnumerable<Product> products = await productRepository.Products();
            List<Product> productsJson = new List<Product>();
            foreach(var p in products)
            {
                productsJson.Add(new Product { ProductId = p.ProductId, Name = p.Name, Measure = p.Measure});
            }
            return Json(productsJson);
        }
    }
}
