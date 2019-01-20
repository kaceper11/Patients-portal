using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.UnitViewModels
{
    public class UnitsViewModel
    {
        public IEnumerable<Unit> Units { get; set; }
    }
}
