using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }


        public async Task<IEnumerable<Category>> Categories()
        {
            return await context.Categories.Include(x => x.Products).ToListAsync();
        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void DeleteCategoryt(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public async Task<ICollection<Category>> FindAllAsync(Expression<Func<Category, bool>> expression)
        {
            return await context.Categories
                .Where(expression).Include(x => x.Products).ToListAsync();
        }

        public async Task<Category> FindAsync(Expression<Func<Category, bool>> expression)
        {
            return await context.Categories
                .Where(expression).Include(x => x.Products).FirstOrDefaultAsync();
        }
    }
}
