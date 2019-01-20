using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.UserViewModels
{
    public class UserDetailsViewModel
    {
        [Required]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string MobileNumber { get; set; }

        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public string Pesel { get; set; }

        public string RoleName { get; set; }

        public string Specialization { get; set; }

        public string TermsApprovedDate { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
