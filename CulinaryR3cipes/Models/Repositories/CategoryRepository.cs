using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext context) : base(context) { }


        public async Task<IEnumerable<Category>> Categories()
        {
            return await Context.Categories.Include(x => x.Products).ToListAsync();
        }

        public async Task<ICollection<Category>> FindAllAsync(Expression<Func<Category, bool>> expression)
        {
            return await Context.Categories
                .Where(expression).Include(x => x.Products).ToListAsync();
        }

        public async Task<Category> FindAsync(Expression<Func<Category, bool>> expression)
        {
            return await Context.Categories
                .Where(expression).Include(x => x.Products).FirstOrDefaultAsync();
        }
    }
}
