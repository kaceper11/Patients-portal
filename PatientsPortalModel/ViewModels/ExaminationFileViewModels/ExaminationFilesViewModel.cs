using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.ExaminationFileViewModels
{
    public class ExaminationFilesViewModel
    {
        public IEnumerable<ExaminationFile> ExaminationFiles { get; set; }
    }
}
