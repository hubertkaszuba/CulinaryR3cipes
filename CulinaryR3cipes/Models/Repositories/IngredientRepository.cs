using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        ApplicationDbContext context;

        public IngredientRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Ingredient> Ingredients => context.Ingredients
            .Include(c=>c.Product)
            .Include(c=>c.Recipe);

        public void AddIngredients(IEnumerable<Ingredient> ingredients)
        {
            context.AddRange(ingredients);
            context.SaveChanges();
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            context.Remove(ingredient);
            context.SaveChanges();
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            context.Update(ingredient);
            context.SaveChanges();
        }
    }
}
