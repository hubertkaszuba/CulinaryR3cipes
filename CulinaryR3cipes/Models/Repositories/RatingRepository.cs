using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(Rating rating)
        {
            _context.Remove(rating);
            _context.SaveChanges();
        }

        public void DeleteRating(Expression<Func<Rating, bool>> expression)
        {
            _context.Remove(_context.Ratings.Where(expression).FirstOrDefault());
            _context.SaveChanges();
        }

        public async Task<ICollection<Rating>> FindAllAsync(Expression<Func<Rating, bool>> expression)
        {
            return await _context.Ratings.Where(expression)
             .Include(rating => rating.Recipe)
             .Include(rating => rating.User).ToListAsync();
        }

        public async Task<Rating> FindAsync(Expression<Func<Rating, bool>> expression)
        {
            return await _context.Ratings.Where(expression)
             .Include(rating => rating.Recipe)
             .Include(rating => rating.User).FirstOrDefaultAsync();
        }

        public async Task<List<Rating>> Ratings()
        {
            return await _context.Ratings
                .Include(rating => rating.Recipe)
                .Include(rating => rating.User).ToListAsync();
        }
    }
}
