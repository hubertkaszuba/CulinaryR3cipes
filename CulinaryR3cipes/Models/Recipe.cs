﻿using System;
using System.Collections.Generic;

namespace CulinaryR3cipes.Models
{
    public partial class Recipe
    {
        public int RecipeId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Time { get; set; }
        public byte[] Img { get; set; }

        public Type Type { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
