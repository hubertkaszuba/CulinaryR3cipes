using CulinaryR3cipes.Models;
using CulinaryR3cipes.Models.Interfaces;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        IProductRepository productRepository;
        IRecipeRepository recipeRepository;
        ITypeRepository typeRepository;

        public RecipeController(IProductRepository product, IRecipeRepository recipe, ITypeRepository type)
        {
            productRepository = product;
            recipeRepository = recipe;
            typeRepository = type;
        }

        public async Task<IActionResult> Recipe()
        {
            return View(new RecipeViewModel { Types = await typeRepository.Types()});
        }

        [HttpPost]
        public async Task<IActionResult> Recipe (RecipeViewModel model)
        {
            if (model.Image != null)
            {
                foreach (var item in model.Image)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            model.Recipe.Img = stream.ToArray();
                        }
                    }
                }
            }
            IEnumerable<Product> productsToIngredients = await productRepository.FindAllAsync(p => model.Ingredients.Any(i => i.ProductId == p.ProductId));
            model.Recipe.Ingredients = new List<Ingredient>();
            foreach(var product in productsToIngredients)
            {
                model.Recipe.Ingredients.Add(new Ingredient { Product = product, Quantity = model.Ingredients.Where(i => i.ProductId == product.ProductId).First().Quantity });
            }

            recipeRepository.AddRecipe(model.Recipe);
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
