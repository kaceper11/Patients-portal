using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.Entities;
using PatientsPortalModel.ViewModels.ExaminationFileViewModels;

namespace PatientsPortal.Controllers
{
    public class ExaminationFileController : Controller
    {
        private PatientsPortalContext db = new PatientsPortalContext();

        public ExaminationFileController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        [CustomAuthorize(Roles = "Administrator, Registration, Patient")]
        public ActionResult Index()
        {
            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator") || Roles.IsUserInRole(Membership.GetUser().UserName, "Registration"))
            {
                var modelForAdmin = new ExaminationFilesViewModel()
                {
                    ExaminationFiles = db.ExaminationFiles.ToList()
                };
                return View(modelForAdmin);
            }

            int userId = Int32.Parse(Membership.GetUser().ProviderUserKey.ToString());

            var modelForPatient = new ExaminationFilesViewModel()
            {
                ExaminationFiles = db.ExaminationFiles.Where(m => m.UserId == userId).ToList()
            };
            return View(modelForPatient);
        }

        public FileResult Download(int id)

        {
            var FileById = (from FC in db.ExaminationFiles
                where FC.Id.Equals(id)
                select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "application/pdf", FileById.FileName+".pdf");
        }

        [HttpGet]
        [CustomAuthorize(Roles = "Administrator, Registration")]
        public ActionResult Upload()
        {
            var patients = db.Users.Where(m => m.Roles.Select(r => r.RoleName == "Patient").FirstOrDefault());

            var model = new ExaminationFileDetailsViewModel()
            {
                Users = patients.ToList(),
                Doctors = db.Doctors.ToList()
            };           

            if (db.Doctors.Any() && patients.Any())
            {
                return View(model);
            }
            TempData["ExaminationFileNotCreated"] = "Żaden lekarz lub pacjent nie zostali jeszcze dodani";
            return RedirectToAction("Index", "ExaminationFile");
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator, Registration")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(ExaminationFileDetailsViewModel model, HttpPostedFileBase examinationUpload)
        {
            if (examinationUpload != null)
            {
                model.FileContent = new byte[examinationUpload.ContentLength];
                examinationUpload.InputStream.Read(model.FileContent, 0, examinationUpload.ContentLength);
            }
          
            var patients = db.Users.Where(m => m.Roles.Select(r => r.RoleName == "Patient").FirstOrDefault());

            model.Users = patients.ToList();
            model.Doctors = db.Doctors.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var file = new ExaminationFile()
            {
                User = model.User,
                Doctor = model.Doctor, 
                UserId = model.UserId,
                DoctorId = model.DoctorId,
                FileContent = model.FileContent,
                FileName = model.FileName,
                Comment = model.Comment,
                CreatedDate = DateTime.Now,
                CreatedBy = Membership.GetUser().UserName
            };

            {
                db.ExaminationFiles.Add(file);
                db.SaveChanges();
            }

            TempData["ExaminationFileCreated"] = "Badanie - plik zostało dodane";
            return RedirectToAction("Index", "ExaminationFile");
        }
    }
}
