using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PatientsPortalModel.Entities
{
    [TrackChanges]

    public class ExaminationFile
    {
        public int Id { get; set; }

        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }

        public virtual int? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }

        public string Comment { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
