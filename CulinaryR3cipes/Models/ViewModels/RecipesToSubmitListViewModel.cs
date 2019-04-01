using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class RecipesToSubmitListViewModel
    {
        public IEnumerable<Recipe> RecipesToSubmit { get; set; }
    }
}
