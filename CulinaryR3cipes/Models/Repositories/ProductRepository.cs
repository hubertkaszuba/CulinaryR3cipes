using CulinaryR3cipes.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<Product>>Products()
        {
            return await Context.Products
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(c => c.Category).ToListAsync();
        }

        public async Task<ICollection<Product>> FindAllAsync(Expression<Func<Product, bool>> expression)
        {
            return await Context.Products.Where(expression)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(c => c.Category).ToListAsync();
        }

        public async Task<Product> FindAsync(Expression<Func<Product, bool>> expression)
        {
            return await Context.Products.Where(expression)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(c => c.Category).FirstOrDefaultAsync();
        }
    }
}
