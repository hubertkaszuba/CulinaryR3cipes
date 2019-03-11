using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface IIngredientRepository
    {
        IQueryable<Ingredient> Ingredients { get; }

        void AddIngredients(IEnumerable<Ingredient> ingredients);
        void DeleteIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);
    }
}
