﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public class Favourite
    {
        public int FavouriteId { get; set; }
        public int RecipeId { get; set; }
        
        public Recipe Recipe { get; set; }
        public User User { get; set; }
    }
}
