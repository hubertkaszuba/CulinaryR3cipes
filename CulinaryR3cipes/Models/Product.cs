using System;
using System.Collections.Generic;

namespace CulinaryR3cipes.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Measure { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Fridge> Fridges { get; set; }
    }
}
