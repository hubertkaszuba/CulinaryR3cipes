using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface ITypeRepository
    {
        Task<IEnumerable<Type>> Types();
        Task<ICollection<Type>> FindAllAsync(Expression<Func<Type, bool>> expression);
        Task<Type> FindAsync(Expression<Func<Type, bool>> expression);
        void AddType(Type type);
        void DeleteType(Type type);
        void UpdateType(Type type);
    }
}
