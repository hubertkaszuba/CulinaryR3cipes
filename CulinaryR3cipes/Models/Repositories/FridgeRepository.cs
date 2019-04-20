using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class FridgeRepository : BaseRepository<Fridge>, IFridgeRepository
    {
        public FridgeRepository(ApplicationDbContext context) : base(context) { }

        public async Task <IEnumerable<Fridge>> Fridges()
        {
            return await Context.Fridges
            .Include(x => x.Product)
            .Include(x => x.User).ToListAsync();
        }

        public async Task <IEnumerable<Fridge>> GetUserProducts(User user)
        {
            return await Context.Fridges
            .Include(x => x.Product)
            .Include(x => x.User)
            .Where(f => f.User == user).ToListAsync();
        }

        public async Task<ICollection<Fridge>> FindAllAsync(Expression<Func<Fridge, bool>> expression)
        {
            return await Context.Fridges.Where(expression)
            .Include(x => x.Product)
            .Include(x => x.User).ToListAsync();
        }

        public async Task<Fridge> FindAsync(Expression<Func<Fridge, bool>> expression)
        {
            return await Context.Fridges.Where(expression)
            .Include(x => x.Product)
            .Include(x => x.User).FirstOrDefaultAsync();
        }
    }
}
