using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class FridgeViewModel
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

        public IEnumerable<Fridge> Fridges { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
