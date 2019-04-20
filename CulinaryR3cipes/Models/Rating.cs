using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public class Rating : BaseModel
    {
        public string Comment { get; set; }
        public decimal RatingValue { get; set; }
        public int ReportsCounter { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
