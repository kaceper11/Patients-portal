using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.Entities
{
    [TrackChanges]

    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? BirthDate { get; set; }
       
        public string Gender { get; set; }

        public string MobileNumber { get; set; }

        public string Pesel { get; set; }

        public bool IsActive { get; set; }

        public bool TermsApproved { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? TermsApprovedDate { get; set; }

        public virtual ICollection<Role> Roles { get; set; } 

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
