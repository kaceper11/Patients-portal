using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.ProblemViewModels
{
    public class EditProblemViewModel
    {
        public int Id { get; set; }

        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Temat")]
        [MaxLength(50, ErrorMessage = "Pole 'Temat' może zawierać maksymalnie 50 znaków")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data wystąpienia problemu")]
        public DateTime? ProblemDate { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Opis")]
        [MaxLength(255, ErrorMessage = "Pole 'Opis' może zawierać maksymalnie 255 znaków")]
        public string Description { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
