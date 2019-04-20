using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Ingredient>> Ingredients()
        {
            return await Context.Ingredients
              .Include(c => c.Product)
              .Include(c => c.Recipe).ToListAsync();
        }

        public void AddIngredients(IEnumerable<Ingredient> ingredients)
        {
            Context.AddRange(ingredients);
            Context.SaveChanges();
        }

        public async Task<ICollection<Ingredient>> FindAllAsync(Expression<Func<Ingredient, bool>> expression)
        {
            return await Context.Ingredients.Where(expression)
              .Include(c => c.Product)
              .Include(c => c.Recipe).ToListAsync();
        }

        public async Task<Ingredient> FindAsync(Expression<Func<Ingredient, bool>> expression)
        {
            return await Context.Ingredients.Where(expression)
              .Include(c => c.Product)
              .Include(c => c.Recipe).FirstOrDefaultAsync();
        }
    }
}
