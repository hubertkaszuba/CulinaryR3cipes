using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class FavouriteRepository : BaseRepository<Favourite>, IFavouriteRepository
    {
        public FavouriteRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Favourite>> FindAllAsync(Expression<Func<Favourite, bool>> expression)
        {
            return await Context.Favourites
                .Include(f => f.Recipe)
                .Include(f => f.User)
                .Where(expression).ToListAsync();
        }

        public async Task<Favourite> FindAsync(Expression<Func<Favourite, bool>> expression)
        {
            return await Context.Favourites
            .Include(f => f.Recipe)
            .Include(f => f.User)
            .Where(expression).FirstOrDefaultAsync();
        }
    }
}
