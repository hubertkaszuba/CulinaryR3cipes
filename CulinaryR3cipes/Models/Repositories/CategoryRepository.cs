using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Category> Categories => context.Categories.Include(x => x.Products);

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
    }
}
