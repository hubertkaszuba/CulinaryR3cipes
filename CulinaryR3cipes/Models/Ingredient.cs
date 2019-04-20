using System;
using System.Collections.Generic;

namespace CulinaryR3cipes.Models
{
    public class Ingredient : BaseModel
    {
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
