using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class FavouritesViewModel
    {
        public IEnumerable<Recipe> FavouriteRecipes { get; set; }
    }
}
