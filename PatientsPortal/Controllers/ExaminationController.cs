using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.Entities;
using PatientsPortalModel.ViewModels.ExaminationViewModels;

namespace PatientsPortal.Controllers
{
    public class ExaminationController : Controller

    {
        private readonly PatientsPortalContext db = new PatientsPortalContext();

        public ExaminationController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        [CustomAuthorize(Roles = "Administrator, Patient, Registration")]
        public ActionResult Index()
        {
            if(Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator") || Roles.IsUserInRole(Membership.GetUser().UserName, "Registration"))
            {
                var examinationsForAdmin = new ExaminationsViewModel()
                {
                    Examinations = db.Examinations.ToList()
                };
                return View(examinationsForAdmin);
            }

            int userId = Int32.Parse(Membership.GetUser().ProviderUserKey.ToString());

            var examinationsForPatient = new ExaminationsViewModel()
            {
                Examinations = db.Examinations.Where(m => m.UserId == userId).ToList()
            };

            return View(examinationsForPatient);       
        }


        [CustomAuthorize(Roles = "Administrator, Patient, Registration")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((IfUserIsOpeningHisExamination(id) && Roles.IsUserInRole(Membership.GetUser().UserName, "Patient"))
                || Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator") 
                || Roles.IsUserInRole(Membership.GetUser().UserName, "Registration"))
            {
                var model = db.Examinations.FirstOrDefault(m => m.Id == id);

                var examination = new ExaminationDetailsViewModel()
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    User = model.User,
                    DoctorId = model.DoctorId,
                    Doctor = model.Doctor,
                    UnitId = model.UnitId,
                    Unit = model.Unit,
                    ExaminationName = model.ExaminationName,
                    ExaminationDate = Methods.Methods.ConvertDateToDateWithoutTime(model.ExaminationDate),
                    Result = model.Result,
                    Norm = model.Norm,
                    Description = model.Description,

                    CreatedDate = Methods.Methods.ConvertDateTo24HFormat(model.CreatedDate),
                    CreatedBy = model.CreatedBy,
                    ModifiedDate = Methods.Methods.ConvertDateTo24HFormat(model.ModifiedDate),
                    ModifiedBy = model.ModifiedBy
                };

                if (examination == null)
                {
                    return HttpNotFound();
                }

                return View(examination);
            }
            else
            {
                return RedirectToAction("Index", "Examination"); ;
            }


        }

        [CustomAuthorize(Roles = "Administrator, Registration")]
        public async Task<ActionResult> Create()
        {

            var patients = db.Users.Where(m => m.Roles.Select(r => r.RoleName == "Patient").FirstOrDefault());
            var model = new CreateExaminationViewModel()
            {
                Users = patients.ToList(),
                Doctors = db.Doctors.ToList(),
                Units = db.Units.ToList()
            };            

            if (db.Doctors.Any() && patients.Any() && db.Units.Any())
            {
                return View(model);
            }
            TempData["ExaminationNotCreated"] = "Żaden lekarz lub pacjent nie zostali jeszcze dodani";
            return RedirectToAction("Index", "Examination");
        }

        public bool IfUserIsOpeningHisExamination(int? id)
        {
            var userId =
                (from e in db.Examinations
                    join u in db.Users on e.UserId equals u.UserId
                    where e.Id == id
                    select u.UserId).SingleOrDefault();

            if (userId == (int)Membership.GetUser().ProviderUserKey)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator, Registration")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateExaminationViewModel model)
        {
            var patients = db.Users.Where(m => m.Roles.Select(r => r.RoleName == "Patient").FirstOrDefault());
            model.Doctors = db.Doctors.ToList();
            model.Users = patients.ToList();
            model.Units = db.Units.ToList();

            var examination = new Examination()
            {
                User = model.User,
                Doctor = model.Doctor,
                Unit = model.Unit,
                UserId = model.UserId,
                DoctorId = model.DoctorId,
                UnitId = model.UnitId,
                ExaminationName = model.ExaminationName,
                ExaminationDate = model.ExaminationDate,
                Result = model.Result,
                Norm = model.Norm,
                Description = model.Description,
                
                CreatedDate = DateTime.Now,
                CreatedBy = Membership.GetUser().UserName,
                ModifiedDate = null,
                ModifiedBy = null

            };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            {
                db.Examinations.Add(examination);
                db.SaveChanges();
            }

            TempData["ExaminationCreated"] = "Badanie zostało dodane";
            return RedirectToAction("Index", "Examination");
        }

        [CustomAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var examination = db.Examinations.Find(id);
            var patients = db.Users.Where(m => m.Roles.Select(r => r.RoleName == "Patient").FirstOrDefault());

            var model = new EditExaminationViewModel()
            {
                Id = examination.Id,
                Users = patients.ToList(),
                Doctors = db.Doctors.ToList(),
                Units = db.Units.ToList(),
                UserId = examination.UserId,
                User = examination.User,
                DoctorId = examination.DoctorId,
                Doctor = examination.Doctor,
                ExaminationName = examination.ExaminationName,
                ExaminationDate = examination.ExaminationDate,
                Result = examination.Result,
                UnitId = examination.UnitId,
                Unit = examination.Unit,
                Norm = examination.Norm,
                Description = examination.Description
                
            };

            if (examination == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditExaminationViewModel model)
        {
            var examination = db.Examinations.Find(model.Id);
            var patients = db.Users.Where(m => m.Roles.Select(r => r.RoleName == "Patient").FirstOrDefault());
            model.Doctors = db.Doctors.ToList();
            model.Users = patients.ToList();
            model.Units = db.Units.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            examination.Id = model.Id;
            examination.UserId = model.UserId;
            examination.DoctorId = model.DoctorId;
            examination.ExaminationName = model.ExaminationName;
            examination.ExaminationDate = model.ExaminationDate;
            examination.Result = model.Result;
            examination.UnitId = model.UnitId;
            examination.Norm = model.Norm;
            examination.Description = model.Description;


            examination.ModifiedDate = DateTime.Now;
            examination.ModifiedBy = Membership.GetUser().UserName;

            db.SaveChanges();

            TempData["ExaminationUpdated"] = "Badanie zostało zapisane";
            return RedirectToAction("Index");
        }
    }
}
