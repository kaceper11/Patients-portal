using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.Entities;
using PatientsPortalModel.ViewModels.WorkHoursViewModels;

namespace PatientsPortal.Controllers
{
    public class WorkHourController : Controller
    {
            
        private PatientsPortalContext db = new PatientsPortalContext();

        public WorkHourController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        [CustomAuthorize(Roles = "Administrator, Doctor")]
        public ActionResult Index()
        {

            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator"))
            {
                var workHours = new WorkHoursViewModel()
                {
                    WorkHours = db.WorkHours.ToList()
                };

                return View(workHours);
            }

            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Doctor"))
            {
                int doctorId = GetDoctorIdForUser((int)Membership.GetUser().ProviderUserKey);
                var todayDate = Convert.ToDateTime(DateTime.Now).Date;
                var workHours = new WorkHoursViewModel()
                {                 
                    WorkHours = db.WorkHours.Where(m => 
                            m.DoctorId == doctorId 
                            && m.WorkDate >= todayDate).ToList()
                };

                return View(workHours);
            }

            return View();
        }

        public bool CheckIfWorkHourExist(WorkHour workHour)
        {
            var work = (from w in db.WorkHours
                where w.WorkDate == workHour.WorkDate && w.DoctorId == workHour.DoctorId
                select w);

            if (work.Any())
            {
                return true;
            }

            return false;

        }

        public int GetDoctorIdForUser(int userId)
        {
            var doctorId = (from d in db.Doctors
                where d.UserId == userId
                select d.Id).SingleOrDefault();

            return doctorId;
        }

        [CustomAuthorize(Roles = "Administrator, Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.WorkHours.Find(id);

    
            var workHour = new WorkHourDetailsViewModel()
            {
                WorkDate = model.WorkDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Doctor = model.Doctor,

                CreatedDate = Methods.Methods.ConvertDateTo24HFormat(model.CreatedDate),
                CreatedBy = model.CreatedBy,
                ModifiedDate = Methods.Methods.ConvertDateTo24HFormat(model.ModifiedDate),
                ModifiedBy = model.ModifiedBy
            };

            if (workHour == null)
            {
                return HttpNotFound();
            }

            return View(workHour);
        }

        [CustomAuthorize(Roles = "Administrator, Doctor")]
        public async Task<ActionResult> Create()
        {
            var workHours = new CreateWorkHourViewModel()
            {
                Doctors = db.Doctors.ToList(),
                StartTime = TimeSpan.FromMinutes(480),
                EndTime = TimeSpan.FromMinutes(960)
            };
            if (db.Doctors.Any())
            {
                return View(workHours);
            }
            TempData["WorkHourNotCreated"] = "Żaden lekarz nie został jeszcze dodany";
            return RedirectToAction("Index", "WorkHour");
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator, Doctor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateWorkHourViewModel model)
        {
            model.Doctors = db.Doctors.ToList();
            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator"))
            {
                var workHour = new WorkHour()
                {
                    WorkDate = model.WorkDate,
                    StartTime = Methods.Methods.ConvertTimeSpanToInt(model.StartTime),
                    EndTime = Methods.Methods.ConvertTimeSpanToInt(model.EndTime),
                    DoctorId = model.DoctorId,
                    Doctor = model.Doctor,

                    CreatedDate = DateTime.Now,
                    CreatedBy = Membership.GetUser().UserName,
                    ModifiedDate = null,
                    ModifiedBy = null
                };

                if (!CheckIfWorkHourExist(workHour))
                {
                    if (Methods.Methods.ConvertTimeSpanToInt(model.StartTime) 
                        >= Methods.Methods.ConvertTimeSpanToInt(model.EndTime))
                    {
                        ModelState.AddModelError("", "Czas rozpoczęcia pracy nie może być póżniejszy od czasu zakończenia pracy");
                        return View(model);
                    }

                    db.WorkHours.Add(workHour);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Podany dzień pracy już istnieje");
                    return View(model);
                }

            }

            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Doctor"))
            {
                var workHour = new WorkHour()
                {
                    WorkDate = model.WorkDate,
                    StartTime = Methods.Methods.ConvertTimeSpanToInt(model.StartTime),
                    EndTime = Methods.Methods.ConvertTimeSpanToInt(model.EndTime),
                    DoctorId = GetDoctorIdForUser((int) Membership.GetUser().ProviderUserKey),
                   

                    CreatedDate = DateTime.Now,
                    CreatedBy = Membership.GetUser().UserName,
                    ModifiedDate = null,
                    ModifiedBy = null
                };

                if (!CheckIfWorkHourExist(workHour))
                {
                    if (Methods.Methods.ConvertTimeSpanToInt(model.StartTime)
                        >= Methods.Methods.ConvertTimeSpanToInt(model.EndTime))
                    {
                        ModelState.AddModelError("", "Czas rozpoczęcia pracy nie może być póżniejszy od czasu zakończenia pracy");
                        return View(model);
                    }

                    db.WorkHours.Add(workHour);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Podana dzień pracy już istnieje");
                    return View(model);
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                TempData["WorkHourCreated"] = "Godzina pracy została dodana";
                return RedirectToAction("Index", "WorkHour");
            }
            TempData["WorkHourCreated"] = "Godzina pracy została dodana";
            return RedirectToAction("Index", "WorkHour");
        }

        [CustomAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var workHour = db.WorkHours.Find(id);

            var model = new EditWorkHourViewModel()
            {
                Id = workHour.Id,
                WorkDate = workHour.WorkDate,
                StartTime = Methods.Methods.ConvertIntToTimeSpan(workHour.StartTime),
                EndTime = Methods.Methods.ConvertIntToTimeSpan(workHour.EndTime),
                Doctors = db.Doctors.ToList(),
                Doctor = workHour.Doctor,
                DoctorId = workHour.DoctorId
            };

            if (workHour == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditWorkHourViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            model.Doctors = db.Doctors.ToList();

            var workHour = db.WorkHours.Find(model.Id);
            if (workHour.WorkDate == model.WorkDate && model.DoctorId == workHour.DoctorId)
            {
                if (Methods.Methods.ConvertTimeSpanToInt(model.StartTime)
                    >= Methods.Methods.ConvertTimeSpanToInt(model.EndTime))
                {
                    ModelState.AddModelError("", "Czas rozpoczęcia pracy nie może być póżniejszy od czasu zakończenia pracy");
                    return View(model);
                }

                workHour.WorkDate = model.WorkDate;
                workHour.StartTime = Methods.Methods.ConvertTimeSpanToInt(model.StartTime);
                workHour.EndTime = Methods.Methods.ConvertTimeSpanToInt(model.EndTime);
                workHour.DoctorId = model.DoctorId;

                workHour.ModifiedDate = DateTime.Now;
                workHour.ModifiedBy = Membership.GetUser().UserName;

                db.SaveChanges();
            }
            else if(!CheckIfWorkHourExist(workHour))
            {
                if (Methods.Methods.ConvertTimeSpanToInt(model.StartTime)
                    >= Methods.Methods.ConvertTimeSpanToInt(model.EndTime))
                {
                    ModelState.AddModelError("", "Czas rozpoczęcia pracy nie może być póżniejszy od czasu zakończenia pracy");
                    return View(model);
                }

                workHour.WorkDate = model.WorkDate;
                workHour.StartTime = Methods.Methods.ConvertTimeSpanToInt(model.StartTime);
                workHour.EndTime = Methods.Methods.ConvertTimeSpanToInt(model.EndTime);
                workHour.DoctorId = model.DoctorId;

                workHour.ModifiedDate = DateTime.Now;
                workHour.ModifiedBy = Membership.GetUser().UserName;

                db.SaveChanges();

            }
            
            else
            {
                ModelState.AddModelError("", "Podany dzień pracy już istnieje");
                return View(model);
            }
            TempData["WorkHourUpdated"] = "Godzina pracy została zapisana";
            return RedirectToAction("Index", "WorkHour");
        }
    }
}

