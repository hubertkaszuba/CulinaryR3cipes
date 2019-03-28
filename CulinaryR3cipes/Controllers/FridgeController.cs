using CulinaryR3cipes.Models;
using CulinaryR3cipes.Models.Repositories;
using CulinaryR3cipes.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Controllers
{
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

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            Fridge productInFridge = await fridgeRepository.FindAsync(f => f.ProductId == id && f.User == user);

            return View("Fridge", new FridgeViewModel
            {
                ProductId = id.ToString(),
                Quantity = productInFridge.Quantity,
                Products = await productRepository.Products(),
                Fridges = await fridgeRepository.GetUserProducts(user)
            });
        }

        public async Task<IActionResult> AddToFridge(FridgeViewModel fridge)
        {
            User user = await _signInManager.UserManager.GetUserAsync(User);
            Product product = await productRepository.FindAsync(p => p.ProductId == Convert.ToInt64(fridge.ProductId));

            Fridge productInFridge = await fridgeRepository.FindAsync(f => f.ProductId == product.ProductId && f.User.Id == user.Id);

            if (productInFridge != null)
            {
                productInFridge.Quantity = fridge.Quantity;
                fridgeRepository.UpdateFridge(productInFridge);
            }
            else
                fridgeRepository.AddToFridge(new Fridge { Product = product, User = user, Quantity = fridge.Quantity });

            return RedirectToAction("Fridge");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Fridge fridge = await fridgeRepository.FindAsync(x => x.FridgeId == id);
            fridgeRepository.DeleteFromFridge(fridge);
            return RedirectToAction("Fridge");
        }
    }
}
