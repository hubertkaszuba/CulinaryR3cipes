using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface IRatingRepository
    {
        Task<List<Rating>> Ratings();
        Task<ICollection<Rating>> FindAllAsync(Expression<Func<Rating, bool>> expression);
        void DeleteRating(Expression<Func<Rating, bool>> expression);
    }
}
