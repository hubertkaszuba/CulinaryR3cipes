using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public interface IFridgeRepository
    {
        IQueryable<Fridge> Fridges { get; }

        void AddToFridge(Fridge fridge);
        void DeleteFromFridge(Fridge fridge);
        void UpdateFridge(Fridge fridge);
    }
}
