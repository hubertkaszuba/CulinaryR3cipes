using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public string Comment { get; set; }
        public decimal RatingValue { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
