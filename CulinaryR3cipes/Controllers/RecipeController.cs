using CulinaryR3cipes.Models;
using CulinaryR3cipes.Models.Interfaces;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (model.Ingredients == null)
                ModelState.AddModelError("Ingredients", "Należy dodać chociażby jeden składnik");
            if(!ModelState.IsValid)
            {
                if (ModelState["TypeId"].Errors.Count != 0)
                {
                    ModelState.Remove("TypeId");
                    ModelState.SetModelValue("TypeId", new ValueProviderResult("Należy wybrać typ przepisu", CultureInfo.InvariantCulture));
                    ModelState.AddModelError("TypeId", "Należy wybrać typ przepisu");
                }

                model.Types = await typeRepository.Types();
                return View("Recipe", model);
            }

            if (model.Image != null)
            {
                if (model.Image.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await model.Image.CopyToAsync(stream);
                        model.Recipe.Img = stream.ToArray();
                    }
                }
               
            }

            IEnumerable<Product> productsToIngredients = await productRepository.FindAllAsync(p => model.Ingredients.Any(i => i.Id == p.Id));
            model.Recipe.Ingredients = new List<Ingredient>();
            foreach(var product in productsToIngredients)
            {
                model.Recipe.Ingredients.Add(new Ingredient { Product = product, Quantity = model.Ingredients.Where(i => i.Id == product.Id).First().Quantity });
            }
            model.Recipe.Type = await typeRepository.FindAsync(t => t.Id == model.TypeId);
            recipeRepository.Add(model.Recipe);
            return RedirectToAction("Recipe");
        }

        public async Task<JsonResult> GetData()
        {
            IEnumerable<Product> products = await productRepository.Products();
            products = products.OrderBy(p => p.Name);
            List<Product> productsJson = new List<Product>();
            foreach(var p in products)
            {
                productsJson.Add(new Product { Id = p.Id, Name = p.Name, Measure = p.Measure});
            }
            return Json(productsJson);
        }
    }
}
