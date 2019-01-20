using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.ProblemViewModels
{
    public class ProblemDetailsViewModel
    {
        public int Id { get; set; }

        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }

        public string Subject { get; set; }

        public string ProblemDate { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "DateTime2")]
        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public string ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
