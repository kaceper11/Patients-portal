using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.Entities;
using PatientsPortalModel.ViewModels.ProblemViewModels;

namespace PatientsPortal.Controllers
{
    public class ProblemController :  Controller
    {
        private PatientsPortalContext db = new PatientsPortalContext();

        public ProblemController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Index()
        {

            var problems = new ProblemsViewModel()
            {
                Problems = db.Problems.ToList()
            };

            return View(problems);
        }



        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.Problems.Find(id);

            

            var problem = new ProblemDetailsViewModel()
            {
                Subject = model.Subject,
                ProblemDate = Methods.Methods.ConvertDateToDateWithoutTime(model.ProblemDate),
                Description = model.Description,
                UserId = model.UserId,
                User = model.User,
                CreatedDate = Methods.Methods.ConvertDateTo24HFormat(model.CreatedDate),
                CreatedBy = model.CreatedBy,
                ModifiedDate = Methods.Methods.ConvertDateTo24HFormat(model.ModifiedDate),
                ModifiedBy = model.ModifiedBy
            };

            if (problem == null)
            {
                return HttpNotFound();
            }

            return View(problem);
        }

        [CustomAuthorize(Roles = "Administrator, Patient")]
        public async Task<ActionResult> Create()
        {
            CreateProblemViewModel model = new CreateProblemViewModel();
            model.ProblemDate = DateTime.Now;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Administrator, Patient")]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(CreateProblemViewModel model)
        {

            var problem = new Problem()
            {
                Subject = model.Subject,
                Description = model.Description,
                ProblemDate = model.ProblemDate,

                UserId = (int)Membership.GetUser().ProviderUserKey,
                CreatedDate = DateTime.Now,
                CreatedBy = Membership.GetUser().UserName,
                ModifiedDate = null,
                ModifiedBy = null
            };

            if (!ModelState.IsValid)
            {
                return View();
            }

            {
                db.Problems.Add(problem);
                db.SaveChanges();
            }

            TempData["ProblemCreated"] = "Problem został zgłoszony";
            return RedirectToAction("Index", "Home");
        }

        [CustomAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var problem = db.Problems.Find(id);

            var model = new EditProblemViewModel()
            {
                Id = problem.Id,
                ProblemDate = problem.ProblemDate,
                Description = problem.Description,
                Subject = problem.Subject
            };

            if (problem == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Edit(EditProblemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var problem = db.Problems.Find(model.Id);

            problem.Subject = model.Subject;
            problem.ProblemDate = model.ProblemDate;
            problem.Description = model.Description;
            problem.ModifiedDate = DateTime.Now;
            problem.ModifiedBy = Membership.GetUser().UserName;

            db.SaveChanges();

            TempData["ProblemUpdated"] = "Zmiany zostały zapisane";
            return RedirectToAction("Index");
        }
    }
}
