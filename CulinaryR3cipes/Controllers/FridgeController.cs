using CulinaryR3cipes.Models;
using CulinaryR3cipes.Models.Repositories;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Controllers
{
    [Authorize]
    public class FridgeController : Controller
    {
        IFridgeRepository fridgeRepository;
        IProductRepository productRepository;
        private readonly SignInManager<User> _signInManager;

        public FridgeController(IFridgeRepository fridge, IProductRepository product, SignInManager<User> signInManager)
        {
            fridgeRepository = fridge;
            productRepository = product;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Fridge()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            return View(new FridgeViewModel { Products = await productRepository.Products(),
                Fridges = await fridgeRepository.GetUserProducts(user)
            });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            Fridge productInFridge = await fridgeRepository.FindAsync(f => f.Id == id && f.User == user);

            return View("Fridge", new FridgeViewModel
            {
                ProductId = id,
                Quantity = productInFridge.Quantity,
                Products = await productRepository.Products(),
                Fridges = await fridgeRepository.GetUserProducts(user)
            });
        }

        public async Task<IActionResult> AddToFridge(FridgeViewModel fridge)
        {
            User user = await _signInManager.UserManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                if(ModelState["ProductId"] != null)
                {
                    ModelState.Remove("ProductId");
                    ModelState.SetModelValue("ProductId", new ValueProviderResult("Należy wybrać produkt", CultureInfo.InvariantCulture));
                    ModelState.AddModelError("ProductId", "Należy wybrać produkt");
                }

                fridge.Products = await productRepository.Products();
                fridge.Fridges = await fridgeRepository.GetUserProducts(user);
                return View("Fridge", fridge);
            }
                
            Product product = await productRepository.FindAsync(p => p.Id == fridge.ProductId);

            Fridge productInFridge = await fridgeRepository.FindAsync(f => f.Id == product.Id && f.User.Id == user.Id);

            if (productInFridge != null)
            {
                productInFridge.Quantity = fridge.Quantity;
                fridgeRepository.Update(productInFridge);
            }
            else
                fridgeRepository.Add(new Fridge { Product = product, User = user, Quantity = fridge.Quantity });

            return RedirectToAction("Fridge");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            Fridge fridge = await fridgeRepository.FindAsync(x => x.Id == id);
            fridgeRepository.Remove(fridge);
            return RedirectToAction("Fridge");
        }
    }
}
