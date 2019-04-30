using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Podaj nazwę użytkownika")]
        [Display(Name = "Nazwa użytkownika")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Podaj email")]
        [Display(Name = "E-mail użytkownika")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        [Display(Name = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Hasła nie są identyczne")]
        public string PasswordConfirm { get; set; }
    }
}
