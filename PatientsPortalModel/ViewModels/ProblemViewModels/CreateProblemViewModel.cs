using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.ProblemViewModels
{
    public class CreateProblemViewModel
    {
        [Display(Name = "Temat")]
        [MaxLength(50, ErrorMessage = "Pole 'Temat' może zawierać maksymalnie 50 znaków")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Date, ErrorMessage = "Pole musi być datą")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data wystąpienia problemu")]
        public DateTime? ProblemDate { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Opis")]
        [MaxLength(255, ErrorMessage = "Pole 'Opis' może zawierać maksymalnie 255 znaków")]
        public string Description { get; set; }
    }
}
