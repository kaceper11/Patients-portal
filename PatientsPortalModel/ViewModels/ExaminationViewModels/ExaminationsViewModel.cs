using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.ExaminationViewModels
{
    public class ExaminationsViewModel
    {
        public IEnumerable<Examination> Examinations { get; set; }
    }
}
