﻿using CulinaryR3cipes.Models;
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
            var products = await productRepository.Products();
            products = products.OrderBy(p => p.Name);
            return View(new FridgeViewModel {
                Products = products,
                Fridges = await fridgeRepository.GetUserProducts(user)
            });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            Fridge productInFridge = await fridgeRepository.FindAsync(f => f.Id == id && f.User == user);
            var products = await productRepository.Products();
            products = products.OrderBy(p => p.Name);
            return View("Fridge", new FridgeViewModel
            {
                Id = productInFridge.Product.Id,
                Quantity = productInFridge.Quantity,
                Products = products,
                Fridges = await fridgeRepository.GetUserProducts(user)
            });
        }

        public async Task<IActionResult> AddToFridge(FridgeViewModel fridge)
        {
            User user = await _signInManager.UserManager.GetUserAsync(User);
            var products = await productRepository.Products();
            products = products.OrderBy(p => p.Name);

            if (!ModelState.IsValid)
            {
                if(ModelState["Id"].Errors.Count != 0)
                {
                    ModelState.Remove("Id");
                    ModelState.SetModelValue("Id", new ValueProviderResult("Należy wybrać produkt", CultureInfo.InvariantCulture));
                    ModelState.AddModelError("Id", "Należy wybrać produkt");
                }

                fridge.Products = products;
                fridge.Fridges = await fridgeRepository.GetUserProducts(user);
                return View("Fridge", fridge);
            }
                
            Fridge productInFridge = await fridgeRepository.FindAsync(f => f.Product.Id == fridge.Id && f.User.Id == user.Id);

            if (productInFridge != null)
            {
                productInFridge.Quantity = (int)fridge.Quantity;
                fridgeRepository.Update(productInFridge);
            }
            else
            {
                Product product = await productRepository.FindAsync(p => p.Id == fridge.Id);
                fridgeRepository.Add(new Fridge { Product = product, User = user, Quantity = (int)fridge.Quantity });
            }
            
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
