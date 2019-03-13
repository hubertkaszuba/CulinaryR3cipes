using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class RecipesListViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
