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
    }
}
