using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.ViewModels.UserViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
