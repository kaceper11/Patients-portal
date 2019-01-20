using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.ViewModels.AccountViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Login")]
        [MaxLength(50, ErrorMessage = "Pole 'Login' może zawierać maksymalnie 50 znaków")]
        public string UserName { get; set; }

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
        [MaxLength(50, ErrorMessage = "Pole 'Hasło' może zawierać maksymalnie 50 znaków")]
        [RegularExpression("^(?:(?=.*[a-z])(?:(?=.*[A-Z])(?=.*[\\d\\W])|(?=.*\\W)(?=.*\\d))|(?=.*\\W)(?=.*[A-Z])(?=.*\\d)).{8,}$", 
            ErrorMessage = "Hasło musi składać się z co najmniej 8 znaków, zawierać wielką i małą literę, oraz cyfrę.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła się nie zgadzają")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Numer telefonu")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pole 'Numer telefonu' musi składać się z cyfr")]
        [MaxLength(50, ErrorMessage = "Pole 'Numer telefonu' może zawierać maksymalnie 50 znaków")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data urodzenia")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Płeć")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Pesel")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pole 'Pesel' musi składać się z cyfr")]
        [MaxLength(11, ErrorMessage = "Pole 'Pesel' musi składać się z 11 znaków")]
        public string Pesel{ get; set; }

        [Display(Name = "Zgadzam się na warunki użytkowania")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "To pole jest wymagane")]
        public bool TermsApproved { get; set; }

        public DateTime? TermsApprovedDate { get; set; }
    }
}
