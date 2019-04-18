using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface IBaseRepository<T>
        where T : BaseModel
    {
        ApplicationDbContext Context { get; set; }

        void Add(T model);
        void Remove(T model);
        void Update(T model);

        Task<IEnumerable<T>> GetAll();
    }
}
