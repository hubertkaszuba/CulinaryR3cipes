using CulinaryR3cipes.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        
        public ProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public async Task<IEnumerable<Product>>Products()
        {
            return await context.Products
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(c => c.Category).ToListAsync();
        }
        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }

        public async Task<ICollection<Product>> FindAllAsync(Expression<Func<Product, bool>> expression)
        {
            return await context.Products.Where(expression)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(c => c.Category).ToListAsync();
        }

        public async Task<Product> FindAsync(Expression<Func<Product, bool>> expression)
        {
            return await context.Products.Where(expression)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Product)
              .Include(product => product.Ingredients)
                  .ThenInclude(ingredient => ingredient.Recipe)
              .Include(c => c.Category).FirstOrDefaultAsync();
        }
    }
}
