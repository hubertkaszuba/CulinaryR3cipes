using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class TypeRepository : BaseRepository<Type>, ITypeRepository
    {
        public TypeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Type>> Types()
        {
            return await Context.Types.Include(c => c.Recipes).ToListAsync();
        }

        public async Task<ICollection<Type>> FindAllAsync(Expression<Func<Type, bool>> expression)
        {
            return await Context.Types.Where(expression)
              .Include(c => c.Recipes).ToListAsync();
        }

        public async Task<Type> FindAsync(Expression<Func<Type, bool>> expression)
        {
            return await Context.Types.Where(expression)
                .Include(c => c.Recipes).FirstOrDefaultAsync();
        }
    }
}
