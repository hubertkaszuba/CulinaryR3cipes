using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<Recipe>> Recipes()
        {
            return await context.Recipes
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(recipe => recipe.Ratings)
                  .ThenInclude(users => users.User)
              .Include(recipe => recipe.Type).ToListAsync();
        }

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

        public async Task<ICollection<Recipe>> FindAllAsync(Expression<Func<Recipe, bool>> expression)
        {
            return await context.Recipes.Where(expression)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(recipe => recipe.Ratings)
                  .ThenInclude(users => users.User)
              .Include(recipe => recipe.Type).ToListAsync();
        }

        public async Task<Recipe> FindAsync(Expression<Func<Recipe, bool>> expression)
        {
            return await context.Recipes.Where(expression)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(recipe => recipe.Ratings)
                  .ThenInclude(users => users.User)
              .Include(recipe => recipe.Type).FirstOrDefaultAsync();
        }
    }
}
