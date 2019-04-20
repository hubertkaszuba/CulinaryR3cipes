using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> Ingredients();
        Task<ICollection<Ingredient>> FindAllAsync(Expression<Func<Ingredient, bool>> expression);
        Task<Ingredient> FindAsync(Expression<Func<Ingredient, bool>> expression);
        void AddIngredients(IEnumerable<Ingredient> ingredients);
    }
}
