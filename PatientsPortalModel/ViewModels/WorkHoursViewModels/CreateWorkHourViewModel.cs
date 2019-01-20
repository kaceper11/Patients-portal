using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.WorkHoursViewModels
{
    public class CreateWorkHourViewModel
    {
        public virtual int? DoctorId { get; set; }

        [Display(Name = "Lekarz")]
        public virtual Doctor Doctor { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dzień pracy")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public DateTime? WorkDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Rozpoczęcie pracy")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public TimeSpan? StartTime { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Zakończenie pracy")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public TimeSpan? EndTime { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
