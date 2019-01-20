using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.ViewModels.UserViewModels
{
    public class ChangeUserDataViewModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Imię")]
        [MaxLength(50, ErrorMessage = "Pole 'Imię' może zawierać maksymalnie 50 znaków")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwisko")]
        [MaxLength(50, ErrorMessage = "Pole 'Nazwisko' może zawierać maksymalnie 50 znaków")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Niepoprawny adres email")]
        [MaxLength(50, ErrorMessage = "Pole 'Email' może zawierać maksymalnie 50 znaków")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Numer telefonu")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pole 'Numer telefonu' musi składać się z cyfr")]
        [MaxLength(50, ErrorMessage = "Pole 'Numer telefonu' może zawierać maksymalnie 50 znaków")]
        public string MobileNumber { get; set; }
    }
}
