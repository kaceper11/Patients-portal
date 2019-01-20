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
    public class CancelVisitViewModel
    {
        public int Id { get; set; }

        public virtual int? UserId { get; set; }

        [Display(Name = "Pacjent")]
        public virtual User User { get; set; }

        public virtual int DoctorId { get; set; }

        [Display(Name = "Lekarz")]
        public virtual Doctor Doctor { get; set; }

        [Display(Name = "Specjalizacja")]
        public string Specialization { get; set; }

        [Display(Name = "Zarezerwowana")]
        public bool IsReserved { get; set; }

        [Display(Name = "Data wizyty")]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Godzina wizyty")]
        public int VisitTime { get; set; }
    }
}
