using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        void AddCategory(Category category);
        void DeleteCategoryt(Category category);
        void UpdateCategory(Category category);
    }
}
