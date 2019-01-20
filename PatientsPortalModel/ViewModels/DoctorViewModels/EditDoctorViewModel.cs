using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.ViewModels.DoctorViewModels
{
    public class EditDoctorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Imię")]
        [MaxLength(50, ErrorMessage = "Pole 'Imię' może zawierać maksymalnie 50 znaków")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwisko")]
        [MaxLength(50, ErrorMessage = "Pole 'Nazwisko' może zawierać maksymalnie 50 znaków")]
        public string LastName { get; set; }

        [Display(Name = "Specjalizacja")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(50, ErrorMessage = "Pole 'Specjalizacja' może zawierać maksymalnie 50 znaków")]
        public string Specialization { get; set; }
    }
}
