using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class RecipeDetailsViewModel
    {
        public Recipe Recipe { get; set; }
        public Rating Rating { get; set; }
        public bool DidUserRate { get; set; }
    }
}
