using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Podaj email")]
        [Display(Name = "E-mail użytkownika")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password, ErrorMessage = "Hasło powinno zawierać conajmniej 8 znaków, w tym: jedną dużą literę, jedną małą literę, cyfrę oraz znak specjalny")]
        public string Password { get; set; }
    }
}
