using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.VisitViewModels
{
    public class VisitDetailsViewModel
    {
        public int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public string Specialization { get; set; }

        public bool IsReserved { get; set; }

        public string VisitDate { get; set; }

        public int VisitTime { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
