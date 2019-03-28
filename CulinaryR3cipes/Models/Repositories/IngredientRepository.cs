﻿using CulinaryR3cipes.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        ApplicationDbContext context;

        public IngredientRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<IEnumerable<Ingredient>> Ingredients()
        {
            return await context.Ingredients
              .Include(c => c.Product)
              .Include(c => c.Recipe).ToListAsync();
        }

        public void AddIngredients(IEnumerable<Ingredient> ingredients)
        {
            context.AddRange(ingredients);
            context.SaveChanges();
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            context.Remove(ingredient);
            context.SaveChanges();
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            context.Update(ingredient);
            context.SaveChanges();
        }

        public async Task<ICollection<Ingredient>> FindAllAsync(Expression<Func<Ingredient, bool>> expression)
        {
            return await context.Ingredients.Where(expression)
              .Include(c => c.Product)
              .Include(c => c.Recipe).ToListAsync();
        }

        public async Task<Ingredient> FindAsync(Expression<Func<Ingredient, bool>> expression)
        {
            return await context.Ingredients.Where(expression)
              .Include(c => c.Product)
              .Include(c => c.Recipe).FirstOrDefaultAsync();
        }
    }
}
