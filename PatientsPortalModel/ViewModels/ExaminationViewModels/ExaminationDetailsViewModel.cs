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
    public class ExaminationDetailsViewModel
    {
        [Required]
        public int Id { get; set; }

        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }


        public virtual int? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public string ExaminationDate { get; set; }

        public string ExaminationName { get; set; }

        public string Result { get; set; }

        public virtual int UnitId { get; set; }

        public virtual Unit Unit { get; set; }

        public string Norm { get; set; }

        public string Description { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
