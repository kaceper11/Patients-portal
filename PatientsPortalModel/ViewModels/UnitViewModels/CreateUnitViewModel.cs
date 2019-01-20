using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.ViewModels.UnitViewModels
{
    public class CreateUnitViewModel
    {

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Wartość")]
        [MaxLength(50, ErrorMessage = "Pole 'Wartość' może zawierać maksymalnie 50 znaków")]
        public string Value { get; set; }
    }
}
