using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.VisitViewModels
{
    public class CreateVisitViewModel
    {
        public int Id { get; set; }

        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }

        public virtual int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public string Specialization { get; set; }

        public bool IsReserved { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? VisitDate { get; set; }

        public int VisitTime { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
