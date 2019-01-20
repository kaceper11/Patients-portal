using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientsPortal.Authentication;

namespace PatientsPortal.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(Roles = "Administrator, Patient, Registration, Doctor")]
        public ActionResult Index()
        {
            return View();
        }
    }
}