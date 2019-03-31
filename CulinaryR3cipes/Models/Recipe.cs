using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CulinaryR3cipes.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public int TypeId { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Czas")]
        public int? Time { get; set; }
        [Required]
        [Display(Name = "Obrazek")]
        public byte[] Img { get; set; }
        public bool IsSubmitted { get; set; }
        public int ReportsCounter { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
