using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class RecipeViewModel
    {
        public Recipe Recipe { get; set; }
        [Required]
        public Guid TypeId { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        [Required(ErrorMessage = "Należy dodać obraz")]
        [Display(Name = "Obraz")]
        public IFormFile Image { get; set; }
        public IEnumerable<Type> Types { get; set; }
    }
}
