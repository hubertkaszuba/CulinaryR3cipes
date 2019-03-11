using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface ITypeRepository
    {
        IQueryable<Type> Types { get; }

        void AddType(Type type);
        void DeleteType(Type type);
        void UpdateType(Type type);
    }
}
