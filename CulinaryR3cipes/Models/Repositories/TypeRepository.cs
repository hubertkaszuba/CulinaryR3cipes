using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        ApplicationDbContext context;

        public TypeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<IEnumerable<Type>> Types()
        {
            return await context.Types.Include(c => c.Recipes).ToListAsync();
        }
        public void AddType(Type type)
        {
            context.Add(type);
            context.SaveChanges();
        }

        public void DeleteType(Type type)
        {
            context.Remove(type);
            context.SaveChanges();
        }

        public void UpdateType(Type type)
        {
            context.Update(type);
            context.SaveChanges();
        }

        public async Task<ICollection<Type>> FindAllAsync(Expression<Func<Type, bool>> expression)
        {
            return await context.Types.Where(expression)
              .Include(c => c.Recipes).ToListAsync();
        }

        public async Task<Type> FindAsync(Expression<Func<Type, bool>> expression)
        {
            return await context.Types.Where(expression)
                .Include(c => c.Recipes).FirstOrDefaultAsync();
        }
    }
}
