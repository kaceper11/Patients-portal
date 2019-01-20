using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.Entities
{
    [TrackChanges]

    public class WorkHour
    {
        public int Id { get; set; }

        public virtual int? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? WorkDate { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
