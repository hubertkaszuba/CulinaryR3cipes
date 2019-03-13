using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        ApplicationDbContext context;

        public RecipeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Recipe> Recipes => context.Recipes
             .Include(recipe => recipe.Ingredients)
                .ThenInclude(ingredient => ingredient.Product)
            .Include(recipe => recipe.Ingredients)
                .ThenInclude(ingredient => ingredient.Recipe)
            .Include(recipe => recipe.Type);

        public void AddRecipe(Recipe recipe)
        {
            context.Add(recipe);
            context.SaveChanges();
        }

        public void DeleteRecipe(Recipe recipe)
        {
            context.Remove(recipe);
            context.SaveChanges();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            context.Update(recipe);
            context.SaveChanges();
        }
    }
}
