using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Fridge> Fridges { get; set; }
    }
}
