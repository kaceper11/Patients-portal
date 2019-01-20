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
using PatientsPortalModel.Entities;
using PatientsPortalModel.ViewModels.ProblemViewModels;
using PatientsPortalModel.ViewModels.TermViewModels;

namespace PatientsPortal.Controllers
{
    public class TermController : Controller
    {
        private PatientsPortalContext db = new PatientsPortalContext();

        public TermController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var terms = new TermsViewModel()
            {
                Terms = db.Terms.ToList()
            };

            return View(terms);
        }

        public ActionResult All()
        {
            var terms = new TermsViewModel()
            {
                Terms = db.Terms.ToList()
            };

            return View(terms);
        }

        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.Terms.Find(id);

            var term = new TermDetailsViewModel()
            {
                Name = model.Name,
                Description = model.Description,
                CreatedDate = Methods.Methods.ConvertDateTo24HFormat(model.CreatedDate),
                CreatedBy = model.CreatedBy,
                ModifiedDate = Methods.Methods.ConvertDateTo24HFormat(model.ModifiedDate),
                ModifiedBy = model.ModifiedBy
            };

            if (term == null)
            {
                return HttpNotFound();
            }

            return View(term);
        }

        [CustomAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Administrator, Patient")]
        public async Task<ActionResult> Create(CreateTermViewModel model)
        {

            var term = new Term()
            {
                Name = model.Name,
                Description = model.Description,
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
                db.Terms.Add(term);
                db.SaveChanges();
            }

            TempData["TermCreated"] = "Warunki użytkowania zostały dodane";
            return RedirectToAction("Index", "Term");
        }

        [CustomAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var term = db.Terms.Find(id);

            var model = new EditTermViewModel()
            {
                Id = term.Id,
                Name = term.Name,
                Description = term.Description,
            };

            if (term == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Edit(EditTermViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var term = db.Terms.Find(model.Id);

            term.Name = model.Name;
            term.Description = model.Description;
            term.ModifiedDate = DateTime.Now;
            term.ModifiedBy = Membership.GetUser().UserName;

            db.SaveChanges();

            TempData["TermUpdated"] = "Warunki użytkowania zostały zapisane";
            return RedirectToAction("Index");
        }
    }
}

