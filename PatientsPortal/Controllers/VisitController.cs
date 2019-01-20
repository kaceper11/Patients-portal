using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.ViewModels.ExaminationViewModels;
using PatientsPortalModel.ViewModels.VisitViewModels;

namespace PatientsPortal.Controllers
{

    public class VisitController : Controller
    {
        private PatientsPortalContext db = new PatientsPortalContext();

        public VisitController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }


        [CustomAuthorize(Roles = "Administrator, Patient")]
        public ActionResult Search(DateTime? visitStartDate, DateTime? visitEndDate, string specialization, string searchBy )
        {
            var model = new SearchVisitViewModel()
            {
                Visits = db.Visits.OrderBy(p => p.VisitDate)
                    .Where(p => p.Id == null).ToList(),
                Doctors = db.Doctors.ToList(),
            };

            if (visitStartDate > visitEndDate)
            {
                ModelState.AddModelError("", "Data wizyty od nie może być większa od daty wizyty do");
                return View(model);
            }
            else
            {
                if (visitStartDate == null || visitEndDate == null)
                {
                    var searchedVisits = new SearchVisitViewModel()
                    {
                        Visits = db.Visits.OrderBy(p => p.VisitDate)
                                .Where(p => p.Id == null).ToList(),
                        Doctors = db.Doctors.ToList()
                    };

                    TempData["VisitWithoutDates"] = "Podaj zakres wizyty";
                    return View(searchedVisits);
                }

                var end = DateTime.Now.AddDays(30);

                if (searchBy == "8.00 - 12.00")
                {
                    if (visitStartDate.HasValue && visitEndDate.HasValue && specialization != null)
                    {
                        var searchedVisits = new SearchVisitViewModel()
                        {
                            Visits = db.Visits.OrderBy(p => p.VisitDate)
                                .Where(p => p.IsReserved == false && p.VisitDate > DateTime.Now && p.VisitDate >= visitStartDate
                                            && p.VisitDate <= visitEndDate && p.VisitDate <= end && p.Specialization.Contains(specialization) && p.VisitTime <= 720
                                            ).ToList(),
                            Doctors = db.Doctors.ToList()
                        };
                        if (searchedVisits.Visits.Any())
                        {
                            return View(searchedVisits);
                        }
                        TempData["NoVisits"] = "Brak wizyt w podanym terminie";
                        return View(model);

                    }
                }
                else
                {
                    if (visitStartDate.HasValue && visitEndDate.HasValue && specialization != null)
                    {
                        var searchedVisits = new SearchVisitViewModel()
                        {
                            Visits = db.Visits.OrderBy(p => p.VisitDate)
                                .Where(p => p.IsReserved == false && p.VisitDate > DateTime.Now && p.VisitDate >= visitStartDate
                                            && p.VisitDate <= visitEndDate && p.VisitDate <= end && p.Specialization.Contains(specialization) && p.VisitTime >= 720
                                            ).ToList(),
                            Doctors = db.Doctors.ToList()
                        };


                        if (searchedVisits.Visits.Any())
                        {
                            return View(searchedVisits);
                        }
                        TempData["NoVisits"] = "Brak wizyt w podanym terminie";
                        return View(model);
                    }
                }
            }

            return null;
        }


        [CustomAuthorize(Roles = "Administrator, Patient, Doctor")]
        public ActionResult Index()
        {

            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator"))
            {
                var visitsForAdmin = new VisitsViewModel()
                {
                    Visits = db.Visits.OrderByDescending(p => p.CreatedDate)
                        .Where(m => m.IsReserved).ToList()
                };

                return View(visitsForAdmin);
            }

            int userId = Int32.Parse(Membership.GetUser().ProviderUserKey.ToString());
            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Patient"))
            {
                var visitsForPatient = new VisitsViewModel()
                {
                    Visits = db.Visits.Where(m => m.UserId == userId).ToList()
                };

                return View(visitsForPatient);
            }
            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Doctor"))
            {

                DateTime end = DateTime.Now.AddDays(14);
                var visitsForDoctor = new VisitsViewModel()
                {
                    Visits = 
                    (from d in db.Doctors
                    join v in db.Visits on d.Id equals v.DoctorId
                    where d.UserId == userId && v.IsReserved==true 
                                             && v.VisitDate >= DateTime.Now 
                                             && v.VisitDate <= end
                    select v).ToList()
                };

                return View(visitsForDoctor);
            }
            else
            {
                return View();
            }

        }


        [CustomAuthorize(Roles = "Administrator, Patient")]
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var visit = db.Visits.FirstOrDefault(m => m.Id == id);

            var model = new CreateVisitViewModel()
            {
                Id = visit.Id,
                User = visit.User,
                UserId = visit.UserId,
                DoctorId = visit.DoctorId,
                Doctor = visit.Doctor,
                Specialization = visit.Specialization,
                IsReserved = visit.IsReserved,
                VisitDate = visit.VisitDate,
                VisitTime = visit.VisitTime,               
            };

            if (visit == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator, Patient")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVisitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var visit = db.Visits.FirstOrDefault(m => m.Id == model.Id);

            visit.UserId = (int)Membership.GetUser().ProviderUserKey;
           // visit.User = model.User;
            visit.DoctorId = model.DoctorId;
            visit.Doctor = model.Doctor;
            visit.Specialization = model.Specialization;
            visit.IsReserved = true;
            visit.VisitDate = model.VisitDate;
            visit.VisitTime = model.VisitTime;

            visit.ModifiedDate = DateTime.Now;
            visit.ModifiedBy = Membership.GetUser().UserName;

            db.SaveChanges();

            TempData["VisitCreated"] = "Wizyta została umówiona";
            return RedirectToAction("Index");
        }

        public bool IfUserIsCancellingHisVisit(int? id)
        {
            var userId = (from m in db.Visits
                         where m.Id == id
                         select m.UserId).SingleOrDefault();

            if (userId == (int)Membership.GetUser().ProviderUserKey)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        public bool IfUserIsOpeningHisVisit(int? id)
        {
            var userId = (from m in db.Visits
                where m.Id == id
                select m.UserId).SingleOrDefault();


            if (userId == (int)Membership.GetUser().ProviderUserKey)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        [CustomAuthorize(Roles = "Administrator, Patient")]
        public async Task<ActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var visit = db.Visits.Find(id);

            if (((IfUserIsCancellingHisVisit(id) && Roles.IsUserInRole(Membership.GetUser().UserName, "Patient")) 
                || Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator")
                 ) && visit.VisitDate > DateTime.Now.AddDays(1) )
            {
                
                var model = new CancelVisitViewModel()
                    {
                        Id = visit.Id,
                        User = visit.User,
                        UserId = visit.UserId,
                        DoctorId = visit.DoctorId,
                        Doctor = visit.Doctor,
                        Specialization = visit.Specialization,
                        IsReserved = visit.IsReserved,
                        VisitDate = visit.VisitDate,
                        VisitTime = visit.VisitTime

                    };

                if (visit == null)
                {
                    return HttpNotFound();
                }

                return View(model);
               
            }
            else
            {
                TempData["VisitCantBeCancelled"] = "Nie możesz anulować tej wizyty";
                return RedirectToAction("Index", "Visit"); 
            }

        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator, Patient")]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(CancelVisitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var visit = db.Visits.Find(model.Id);

            visit.Id = model.Id;

            visit.IsReserved = model.IsReserved;
            if (model.IsReserved == false)
            {
                visit.UserId = null;
            }
            else
            {
                visit.UserId = model.UserId;
            }
            
            visit.DoctorId = model.DoctorId;
            visit.Specialization = model.Specialization;
            visit.VisitDate = model.VisitDate;
            visit.VisitTime = model.VisitTime;


            visit.ModifiedDate = DateTime.Now;
            visit.ModifiedBy = Membership.GetUser().UserName;

            db.SaveChanges();

            TempData["VisitCancelled"] = "Wizyta została anulowana";
            return RedirectToAction("Index");
        }


        [CustomAuthorize(Roles = "Administrator, Patient, Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.Visits.FirstOrDefault(m => m.Id == id);
            if ((IfUserIsOpeningHisVisit(id) && Roles.IsUserInRole(Membership.GetUser().UserName, "Patient"))
                || Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator")
            )
            {
                var visit = new VisitDetailsViewModel()
                    {
                        Id = model.Id,
                        User = model.User,
                        //UserId = model.UserId,
                        DoctorId = model.DoctorId,
                        Doctor = model.Doctor,
                        Specialization = model.Specialization,
                        IsReserved = model.IsReserved,
                        VisitDate = Methods.Methods.ConvertDateToDateWithoutTime(model.VisitDate),
                        VisitTime = model.VisitTime,

                        CreatedDate = Methods.Methods.ConvertDateTo24HFormat(model.CreatedDate),
                        CreatedBy = model.CreatedBy,
                        ModifiedDate = Methods.Methods.ConvertDateTo24HFormat(model.ModifiedDate),
                        ModifiedBy = model.ModifiedBy
                    };

                if (visit == null)
                {
                    return HttpNotFound();
                }

                return View(visit);
            }
            else
            {
                
                return RedirectToAction("Index", "Visit");
                
            }



        }

    }
}
