using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public interface IProductRepository 
    {
        Task<IEnumerable<Product>> Products();
        Task<ICollection<Product>> FindAllAsync(Expression<Func<Product, bool>> expression);
        Task<Product> FindAsync(Expression<Func<Product, bool>> expression);
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
    }
}
