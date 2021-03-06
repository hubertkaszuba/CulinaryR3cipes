﻿using CulinaryR3cipes.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public interface IFridgeRepository : IBaseRepository<Fridge>
    {
        Task <IEnumerable<Fridge>> Fridges();
        Task <IEnumerable<Fridge>> GetUserProducts(User user);
        Task<ICollection<Fridge>> FindAllAsync(Expression<Func<Fridge, bool>> expression);
        Task<Fridge> FindAsync(Expression<Func<Fridge, bool>> expression);
    }
}
