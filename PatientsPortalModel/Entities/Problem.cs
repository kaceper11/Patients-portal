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

    public class Problem
    {
        public int Id { get; set; }

        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }

        public string Subject { get; set; }

        public DateTime? ProblemDate { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
