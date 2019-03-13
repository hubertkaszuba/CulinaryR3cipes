using System;
using System.Collections.Generic;

namespace CulinaryR3cipes.Models
{
    public partial class Type
    {
        public int TypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
