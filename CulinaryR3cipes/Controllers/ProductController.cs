using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryR3cipes.Models;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryR3cipes.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController (IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult ProductList()
        {
            var x = repository.Products.ToList();
            return View(repository.Products);
        }
    }
}