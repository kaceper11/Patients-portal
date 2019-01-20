using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.TermViewModels
{
    public class TermsViewModel
    {
        public IEnumerable<Term> Terms { get; set; }
    }
}
