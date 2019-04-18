using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    interface IFavouriteRepository : IBaseRepository<Favourite>
    {
        Task<IEnumerable<Favourite>> FindAllAsync(Expression<Func<Favourite, bool>> expression);
        Task<Favourite> FindAsync(Expression<Func<Favourite, bool>> expression);
    }
}
