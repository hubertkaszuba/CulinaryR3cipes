using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CulinaryR3cipes.Models
{
    public class Type : BaseModel
    {
        [Display(Name = "Typ")]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
