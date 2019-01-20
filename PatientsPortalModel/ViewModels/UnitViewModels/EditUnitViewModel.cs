using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.ViewModels.UnitViewModels
{
    public class EditUnitViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(50, ErrorMessage = "Pole 'Wartość' może zawierać maksymalnie 50 znaków")]
        public string Value { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
