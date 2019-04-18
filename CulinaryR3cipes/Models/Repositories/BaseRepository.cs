using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseModel
    {
        public ApplicationDbContext Context { get; set; }

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        private DbSet<T> Entities => Context.Set<T>();

        public void Add(T model)
        {
            model.Id = Guid.NewGuid();
            Entities.Add(model);
            Context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public void Remove(T model)
        {
            Entities.Remove(model);
            Context.SaveChanges();
        }

        public void Update(T model)
        {
            Entities.Update(model);
            Context.SaveChanges();
        }
    }
}
