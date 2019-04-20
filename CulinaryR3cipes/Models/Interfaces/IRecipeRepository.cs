using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> Recipes();
        Task<IEnumerable<Recipe>> FindAllAsync(Expression<Func<Recipe, bool>> expression);
        Task<Recipe> FindAsync(Expression<Func<Recipe, bool>> expression);
    }
}
