using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.ExaminationFileViewModels
{
    public class ExaminationFileDetailsViewModel
    {
        [Display(Name = "Pacjent")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Lekarz")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public virtual int? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public IEnumerable<User> Users { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwa badania")]
        [MaxLength(50, ErrorMessage = "Pole 'Nazwa badania' może zawierać maksymalnie 50 znaków")]
        public string FileName { get; set; }

        [Display(Name = "Plik")]
        public byte[] FileContent { get; set; }

        [Display(Name = "Komentarz")]
        [MaxLength(3000, ErrorMessage = "Pole 'Komentarz' może zawierać maksymalnie 3000 znaków")]
        public string Comment { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
