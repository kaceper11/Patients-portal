using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.ViewModels.UserViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Stare hasło")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła się nie zgadzają")]
        public string ConfirmPassword { get; set; }
    }
}
