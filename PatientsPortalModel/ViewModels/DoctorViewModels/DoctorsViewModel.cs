using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.DoctorViewModels
{
    public class DoctorsViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
