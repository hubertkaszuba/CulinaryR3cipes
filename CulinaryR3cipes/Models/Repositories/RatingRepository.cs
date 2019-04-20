using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class RatingRepository : BaseRepository<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext context) : base(context) { }

        public void DeleteRating(Expression<Func<Rating, bool>> expression)
        {
            Context.Remove(Context.Ratings.Where(expression).FirstOrDefault());
            Context.SaveChanges();
        }

        public async Task<ICollection<Rating>> FindAllAsync(Expression<Func<Rating, bool>> expression)
        {
            return await Context.Ratings.Where(expression)
             .Include(rating => rating.Recipe)
             .Include(rating => rating.User).ToListAsync();
        }

        public async Task<Rating> FindAsync(Expression<Func<Rating, bool>> expression)
        {
            return await Context.Ratings.Where(expression)
             .Include(rating => rating.Recipe)
             .Include(rating => rating.User).FirstOrDefaultAsync();
        }

        public async Task<List<Rating>> Ratings()
        {
            return await Context.Ratings
                .Include(rating => rating.Recipe)
                .Include(rating => rating.User).ToListAsync();
        }
    }
}
