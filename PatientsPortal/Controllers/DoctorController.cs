using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.ViewModels.DoctorViewModels;

namespace PatientsPortal.Controllers
{
    public class DoctorController : Controller
    {
        private PatientsPortalContext db = new PatientsPortalContext();

        public DoctorController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Index()
        {

            var doctors = new DoctorsViewModel()
            {
                Doctors = db.Doctors.ToList()
            };

            return View(doctors);
        }

        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.Doctors.Find(id);  

            var doctor = new DoctorDetailsViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Specialization = model.Specialization,
                WorkHours = db.WorkHours.Where(m => m.DoctorId == id && m.WorkDate.Value.Month == DateTime.Now.Month).ToList(),

                CreatedDate = Methods.Methods.ConvertDateTo24HFormat(model.CreatedDate),
                CreatedBy = model.CreatedBy,
                ModifiedDate = Methods.Methods.ConvertDateTo24HFormat(model.ModifiedDate),
                ModifiedBy = model.ModifiedBy
            };

            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(doctor);
        }

        [CustomAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var doctor = db.Doctors.Find(id);

            var model = new EditDoctorViewModel()
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization
            };

            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditDoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var doctor = db.Doctors.Find(model.Id);

            doctor.FirstName = model.FirstName;
            doctor.LastName = model.LastName;
            doctor.Specialization = model.Specialization;

            doctor.ModifiedDate = DateTime.Now;
            doctor.ModifiedBy = Membership.GetUser().UserName;

            db.SaveChanges();

            TempData["DoctorUpdated"] = "Lekarz został zapisany";
            return RedirectToAction("Index");
        }
    }
}
