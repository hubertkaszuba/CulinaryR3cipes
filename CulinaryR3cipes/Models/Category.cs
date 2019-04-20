using System;
using System.Collections.Generic;

namespace CulinaryR3cipes.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
