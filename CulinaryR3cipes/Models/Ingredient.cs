using System;
using System.Collections.Generic;

namespace CulinaryR3cipes.Models
{
    public partial class Ingredient
    {
        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        public int RecipeId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
