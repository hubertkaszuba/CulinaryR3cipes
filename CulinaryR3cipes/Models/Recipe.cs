using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CulinaryR3cipes.Models
{
    public class Recipe : BaseModel
    {
        [Required(ErrorMessage = "Należy podać nazwę")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Należy podać opis")]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Należy podać czas wykonania przepisu")]
        [Display(Name = "Czas wykonania")]
        public int? Time { get; set; }
        //[Required(ErrorMessage = "Należy dodać obraz")]
        //[Display(Name = "Obraz")]
        public byte[] Img { get; set; }
        public bool IsSubmitted { get; set; }
        public int ReportsCounter { get; set; }
        public decimal AverageRating { get; set; } 

        public virtual Type Type { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}
