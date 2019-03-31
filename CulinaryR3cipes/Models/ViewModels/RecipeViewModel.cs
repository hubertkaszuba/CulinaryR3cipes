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
        public int TypeId { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<IFormFile> Image { get; set; }
        public IEnumerable<Type> Types { get; set; }
    }
}
