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
        [Required(ErrorMessage = "Należy wybrać produkt")]
        [Display(Name = "Produkt")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Należy podać ilość")]
        [Display(Name = "Ilość")]
        [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być większa niż 0")]
        public int? Quantity { get; set; }

        public IEnumerable<Fridge> Fridges { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
