using System;
using System.Collections.Generic;

namespace CulinaryR3cipes.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
