using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> Recipes { get; }

        void AddRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
    }
}
