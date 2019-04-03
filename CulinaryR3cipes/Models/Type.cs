using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CulinaryR3cipes.Models
{
    public partial class Type
    {
        public int TypeId { get; set; }
        [Display(Name = "Typ")]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
