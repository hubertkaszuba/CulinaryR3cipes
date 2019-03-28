using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class FridgeRepository : IFridgeRepository
    {
        private ApplicationDbContext context;

        public FridgeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task <IEnumerable<Fridge>> Fridges()
        {
            return await context.Fridges
            .Include(x => x.Product)
            .Include(x => x.User).ToListAsync();
        }

        public async Task <IEnumerable<Fridge>> GetUserProducts(User user)
        {
            return await context.Fridges
            .Include(x => x.Product)
            .Include(x => x.User)
            .Where(f => f.User == user).ToListAsync();
        }

        public void AddToFridge(Fridge fridge)
        {
            context.Add(fridge);
            context.SaveChanges();
        }

        public void DeleteFromFridge(Fridge fridge)
        {
            context.Remove(fridge);
            context.SaveChanges();
        }

        public void UpdateFridge(Fridge fridge)
        {
            context.Update(fridge);
            context.SaveChanges();
        }

        public async Task<ICollection<Fridge>> FindAllAsync(Expression<Func<Fridge, bool>> expression)
        {
            return await context.Fridges.Where(expression)
            .Include(x => x.Product)
            .Include(x => x.User).ToListAsync();
        }

        public async Task<Fridge> FindAsync(Expression<Func<Fridge, bool>> expression)
        {
            return await context.Fridges.Where(expression)
            .Include(x => x.Product)
            .Include(x => x.User).FirstOrDefaultAsync();
        }
    }
}
