using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        ApplicationDbContext context;

        public TypeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Type> Types => context.Types.Include(c => c.Recipes);

        public void AddType(Type type)
        {
            context.Add(type);
            context.SaveChanges();
        }

        public void DeleteType(Type type)
        {
            context.Remove(type);
            context.SaveChanges();
        }

        public void UpdateType(Type type)
        {
            context.Update(type);
            context.SaveChanges();
        }
    }
}
