using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.ExaminationViewModels
{
    public class CreateExaminationViewModel
    {
        [Display(Name = "Pacjent")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Lekarz")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public virtual int? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data badania")]
        public DateTime? ExaminationDate { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwa badania")]
        [MaxLength(50, ErrorMessage = "Pole 'Nazwa badania' może zawierać maksymalnie 50 znaków")]
        public string ExaminationName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Wynik")]
        [MaxLength(50, ErrorMessage = "Pole 'Wynik' może zawierać maksymalnie 50 znaków")]
        public string Result { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Jednostka")]
        public virtual int UnitId { get; set; }
        
        public virtual Unit Unit { get; set; }

        public IEnumerable<Unit> Units { get; set; }

        public IEnumerable<User> Users { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(50, ErrorMessage = "Pole 'Norma' może zawierać maksymalnie 50 znaków")]
        [Display(Name = "Norma")]
        public string Norm { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Opis")]
        [MaxLength(255, ErrorMessage = "Pole 'Opis' może zawierać maksymalnie 255 znaków")]
        public string Description { get; set; }

    }
}
