using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.VisitViewModels
{
    public class SearchVisitViewModel
    {
        [Required]
        [Display(Name = "Specjalizacja")]
        public string Specialization { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data wizyty od")]
        public DateTime? VisitStartDate { get; set; }

        public IEnumerable<Visit> Visits { get; set; }

        public virtual int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public IEnumerable<Doctor> Doctors{ get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data wizyty do")]
        public DateTime? VisitEndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH-mm}")]
        [Display(Name = "Godzina wizyty od")]
        public int VisitStartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH-mm}")]
        [Display(Name = "Godzina wizyty do")]
        public int VisitEndTime { get; set; }
    }
}
