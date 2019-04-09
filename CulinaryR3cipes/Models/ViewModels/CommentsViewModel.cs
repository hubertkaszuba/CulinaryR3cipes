using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class CommentsViewModel
    {
        public int RecipeId { get; set; }
        public List<Rating> Ratings { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
