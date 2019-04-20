using CulinaryR3cipes.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> Categories();
        Task<ICollection<Category>> FindAllAsync(Expression<Func<Category, bool>> expression);
        Task<Category> FindAsync(Expression<Func<Category, bool>> expression);
    }
}
