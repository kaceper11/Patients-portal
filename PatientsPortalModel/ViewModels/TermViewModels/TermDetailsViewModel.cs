using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.ViewModels.TermViewModels
{
    public class TermDetailsViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "DateTime2")]
        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public string ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
