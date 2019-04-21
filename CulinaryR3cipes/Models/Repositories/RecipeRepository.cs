using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Recipe>> Recipes()
        {
            return await Context.Recipes
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(recipe => recipe.Ratings)
                  .ThenInclude(users => users.User)
              .Include(recipe => recipe.Type)
              .Include(recipe => recipe.Favourites)
                .ThenInclude(users => users.User).ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> FindAllAsync(Expression<Func<Recipe, bool>> expression)
        {
            return await Context.Recipes.Where(expression)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(recipe => recipe.Ratings)
                  .ThenInclude(users => users.User)
              .Include(recipe => recipe.Type)
              .Include(recipe => recipe.Favourites)
                .ThenInclude(users => users.User).ToListAsync();
        }

        public async Task<Recipe> FindAsync(Expression<Func<Recipe, bool>> expression)
        {
            return await Context.Recipes.Where(expression)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(recipe => recipe.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(recipe => recipe.Ratings)
                  .ThenInclude(users => users.User)
              .Include(recipe => recipe.Type)
              .Include(recipe => recipe.Favourites)
                .ThenInclude(users => users.User).FirstOrDefaultAsync();
        }
    }
}
