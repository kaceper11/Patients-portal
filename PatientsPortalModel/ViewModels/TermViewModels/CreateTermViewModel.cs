﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.ViewModels.TermViewModels
{
    public class CreateTermViewModel
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(50, ErrorMessage = "Pole 'Nazwa' może zawierać maksymalnie 50 znaków")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(3000, ErrorMessage = "Pole 'Opis' może zawierać maksymalnie 3000 znaków")]
        public string Description { get; set; }
    }
}
