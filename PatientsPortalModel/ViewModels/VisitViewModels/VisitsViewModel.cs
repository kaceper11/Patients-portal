using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.VisitViewModels
{
    public class VisitsViewModel
    {
        public IEnumerable<Visit> Visits { get; set; }
    }
}
