using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.Entities;
using PatientsPortalModel.ViewModels.UserViewModels;

namespace PatientsPortal.Controllers
{
    public class UserController : Controller
    {

        private PatientsPortalContext db = new PatientsPortalContext();

        public UserController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        public ActionResult Create()
        {
            var model = new CreateUserViewModel()
            {
                Roles = db.Roles.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
           if (ModelState.IsValid)
            {
                string email = Membership.GetUserNameByEmail(model.Email);
                if (!string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("Warning email", "Przykro nam, ale podany email został już użyty");
                    return View(model);
                }

                string username = Methods.Methods.GetUserNameByLogin(model.UserName);
                if (!string.IsNullOrEmpty(username))
                {
                    ModelState.AddModelError("", "Podany login został już użyty");
                    return View(model);
                }

                model.Roles = db.Roles.ToList();

                var user = new User()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    Pesel = model.Pesel,
                    BirthDate = model.BirthDate,
                    Password = model.Password, //Methods.PasswordEncryption.Decrypt(),
                    Gender = model.Gender,
                    IsActive = true,   
                    
                    TermsApproved = model.TermsApproved,
                    TermsApprovedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    ModifiedDate = null,
                    ModifiedBy = null                   
                };


                var doctor = new Doctor()
                {
                    UserId = user.UserId,
                    User = user,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Specialization = model.Specialization,

                    CreatedDate = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    ModifiedDate = null,
                    ModifiedBy = null
                };
                

                db.Users.Add(user);
                db.SaveChanges();
 
                if (user.Roles == null)
                {
                    user.Roles = new List<Role>();
                }

                var role = db.Roles.FirstOrDefault(m => m.RoleId == model.RoleId);
                var roleDoctor = db.Roles.FirstOrDefault(m => m.RoleName == "Doctor");
                user.Roles.Add(role);

                if (user.Roles.Contains(roleDoctor))
                {
                    db.Doctors.Add(doctor);
                }
                db.SaveChanges();


            };

            if (!ModelState.IsValid)
            {             
                return View();
            }
            TempData["UserCreated"] = "Użytkownik został stworzony";
            return RedirectToAction("Index", "User");
        }

        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Index()
        {

            var users = new UsersViewModel()
            {
                Users = db.Users.ToList().Where(m => m.IsActive)
            };

            return View(users);
        }

        [CustomAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(id);

            var model = new EditUserViewModel()
            {
                Id = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                Pesel = user.Pesel,
                BirthDate = user.BirthDate,
                Password = user.Password, //Methods.PasswordEncryption.Encrypt(),
                ConfirmPassword = user.Password,
                Gender = user.Gender,
                TermsApproved = user.TermsApproved,
                TermsApprovedDate = user.TermsApprovedDate,
                Roles = db.Roles 

            };

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (ModelState.IsValid)
            {

                if (!Methods.Methods.GetUserByEmail(model.Email, model.Id))
                {
                    string email = Membership.GetUserNameByEmail(model.Email);
                    if (!string.IsNullOrEmpty(email))
                    {
                        ModelState.AddModelError("", "Podany adres email został już użyty");
                        return View(model);
                    }
                }

                if (!Methods.Methods.GetUserByLogin(model.UserName, model.Id))
                {
                    string username = Methods.Methods.GetUserNameByLogin(model.UserName);
                    if (!string.IsNullOrEmpty(username))
                    {
                        ModelState.AddModelError("", "Podany login został już użyty");
                        return View(model);
                    }
                }

                var user = db.Users.Find(model.Id);

                user.UserId = model.Id;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.MobileNumber = model.MobileNumber;
                user.Pesel = model.Pesel;
                user.Password = model.Password;
                user.BirthDate = model.BirthDate;
                user.Gender = model.Gender;
                user.TermsApproved = model.TermsApproved;
               // user.Roles = model.Roles;
                user.TermsApprovedDate = model.TermsApprovedDate;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = Membership.GetUser().UserName;

                db.SaveChanges();
            }

            TempData["UserUpdated"] = "Użytkownik został zapisany";
            return RedirectToAction("Index");
        }

        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.Users.Find(id);

            var user = new UserDetailsViewModel()
            {
                Id = model.UserId,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                Pesel = model.Pesel,
                BirthDate = Methods.Methods.ConvertDateToDateWithoutTime(model.BirthDate),
                Gender = model.Gender,
                TermsApprovedDate = Methods.Methods.ConvertDateTo24HFormat(model.TermsApprovedDate),
                RoleName = model.Roles.Select(r => r.RoleName).FirstOrDefault(),
                CreatedDate = Methods.Methods.ConvertDateTo24HFormat(model.CreatedDate),
                CreatedBy = model.CreatedBy,
                ModifiedDate = Methods.Methods.ConvertDateTo24HFormat(model.ModifiedDate),
                ModifiedBy = model.ModifiedBy

                //ROLES
            };

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public async Task<ActionResult> ChangeUserData()
        {

            var user = db.Users.Find((int)Membership.GetUser().ProviderUserKey);

            var model = new ChangeUserDataViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
            };

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserData(ChangeUserDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                //Email Verification
                string email = Membership.GetUserNameByEmail(model.Email);
                if (!string.IsNullOrEmpty(email))
                {
                    if (!Methods.Methods.GetUserByEmail(model.Email, (int)Membership.GetUser().ProviderUserKey))
                    {                       
                        ModelState.AddModelError("Warning email", "Podany adres email został już użyty");
                        return View(model);
                    }
                }

                var user = db.Users.Find((int)Membership.GetUser().ProviderUserKey);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.MobileNumber = model.MobileNumber;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = Membership.GetUser().UserName;

                db.SaveChanges();
            }

            TempData["UserDataChanged"] = "Dane osobowe zostały zapisane";
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (ModelState.IsValid)
            {

                var user = db.Users.Find((int)Membership.GetUser().ProviderUserKey);
               
                if (!Methods.Methods.CheckOldPassword((int)Membership.GetUser().ProviderUserKey, model.OldPassword))
                {
                    ModelState.AddModelError("Błędne hasło", "Podane hasło jest nieprawidłowe");
                    return View(model);
                }

                user.Password = model.NewPassword;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = Membership.GetUser().UserName;

                db.SaveChanges();
            }

            TempData["PasswordChanged"] = "Hasło zostało zmienione";
            return RedirectToAction("Index", "Home");
        }

    }
}
