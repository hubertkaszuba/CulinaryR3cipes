using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "E-mail użytkownika")]
        public string Email { get; set; }
    }
}
