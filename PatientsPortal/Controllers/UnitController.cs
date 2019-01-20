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
using PatientsPortalModel.ViewModels.UnitViewModels;

namespace PatientsPortal.Controllers
{
    public class UnitController : Controller
    {
        private static PatientsPortalContext db = new PatientsPortalContext();

        public UnitController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        [CustomAuthorize(Roles = "Administrator, Registration")]
        public ActionResult Index()
        {
            var units = new UnitsViewModel()
            {
                Units = db.Units.ToList()
            };

            return View(units);
        }

        public static bool IsUnitUsed(int? unitId)
        {
            var unit = (from u in db.Examinations
                where u.UnitId == unitId
                select u).Any();

            if (unit)
            {
                return true;
            }

            return false;
        }

        [CustomAuthorize(Roles = "Administrator, Registration")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.Units.Find(id);

            var unit = new UnitDetailsViewModel()
            {
                Id = model.Id,
                Value = model.Value,
                IsUsed = IsUnitUsed(id),
                CreatedDate = Methods.Methods.ConvertDateTo24HFormat(model.CreatedDate),
                CreatedBy = model.CreatedBy,
                ModifiedDate = Methods.Methods.ConvertDateTo24HFormat(model.ModifiedDate),
                ModifiedBy = model.ModifiedBy
            };

            if (unit == null)
            {
                return HttpNotFound();
            }

            return View(unit);
        }

        [CustomAuthorize(Roles = "Administrator, Registration")]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Administrator, Registration")]
        public async Task<ActionResult> Create(CreateUnitViewModel model)
        {

            var unit = new Unit()
            {
                Value = model.Value,

                //REMEMEBER TO CHANGE IT 
       
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
                db.Units.Add(unit);
                db.SaveChanges();
            }

            TempData["UnitCreated"] = "Jednostka została dodana";
            return RedirectToAction("Index");
        }

        [CustomAuthorize(Roles = "Administrator, Registration")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unit = db.Units.Find(id);

            var model = new EditUnitViewModel()
            {
                Id = unit.Id,
                Value= unit.Value
            };

            if (unit == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Administrator, Registration")]
        public ActionResult Edit(EditUnitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var unit = db.Units.Find(model.Id);

            unit.Value = model.Value;
            unit.ModifiedDate = DateTime.Now;
            unit.ModifiedBy = Membership.GetUser().UserName;

            db.SaveChanges();

            TempData["UnitUpdated"] = "Jednostka została zapisana";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator, Registration")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unit = db.Units.Find(id);
            db.Units.Remove(unit);
            db.SaveChanges();

            TempData["UnitDeleted"] = "Jednostka została usunięta";
            return RedirectToAction("Index", "Unit");
        }
    }
}
