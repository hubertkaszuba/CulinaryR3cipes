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

        public IActionResult Fridge()
        {
            var userId =_signInManager.UserManager.GetUserId(User);
            return View(new FridgeViewModel { Products = productRepository.Products,
                Fridges = fridgeRepository.Fridges.Where(f => f.User.Id == userId)
            });
        }

        public IActionResult Edit(int id)
        {
            var userId = _signInManager.UserManager.GetUserId(User);
            Fridge productInFridge = fridgeRepository.Fridges.Where(f => f.ProductId == id && f.User.Id == userId).FirstOrDefault();
            return View("Fridge", new FridgeViewModel
            {
                ProductId = id.ToString(),
                Quantity = productInFridge.Quantity,
                Products = productRepository.Products,
                Fridges = fridgeRepository.Fridges.Where(f => f.User.Id == userId)
            });
        }


        public async Task<IActionResult> AddToFridge(FridgeViewModel fridge)
        {
            User user = await _signInManager.UserManager.GetUserAsync(User);
            Product product = productRepository.Products.Where(p => p.ProductId == Convert.ToInt64(fridge.ProductId)).First();
            

            Fridge productInFridge = fridgeRepository.Fridges.Where(f => f.ProductId == product.ProductId && f.User.Id == user.Id).FirstOrDefault();

            if (productInFridge != null)
            {
                productInFridge.Quantity = fridge.Quantity;
                fridgeRepository.UpdateFridge(productInFridge);
            }
            else
                fridgeRepository.AddToFridge(new Fridge { Product = product, User = user, Quantity = fridge.Quantity });

            return RedirectToAction("Fridge");
        }

        public IActionResult Delete(int id)
        {
            Fridge fridge = fridgeRepository.Fridges.Where(x => x.FridgeId == id).First();
            fridgeRepository.DeleteFromFridge(fridge);
            return RedirectToAction("Fridge");
        }
    }
}
