using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.WorkHoursViewModels
{
    public class WorkHoursViewModel
    {
        public IEnumerable<WorkHour> WorkHours { get; set; }
    }
}
