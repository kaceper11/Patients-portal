using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.WorkHoursViewModels
{
    public class WorkHourDetailsViewModel
    {
        public int Id { get; set; }

        public virtual int? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public DateTime? WorkDate { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        [Column(TypeName = "DateTime2")]
        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public string ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
