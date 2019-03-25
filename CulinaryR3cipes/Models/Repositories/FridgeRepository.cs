using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Fridge> Fridges => context.Fridges
            .Include(x => x.Product)
            .Include(x => x.User);

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
    }
}
