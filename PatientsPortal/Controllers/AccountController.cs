using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.Entities;
using PatientsPortalModel.ViewModels.AccountViewModels;
using PusherServer;

namespace PatientsPortal.Controllers
{
    public class AccountController : Controller
    {
        private PatientsPortalContext db = new PatientsPortalContext();

        public AccountController()
        {
            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.UserName, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.UserName, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Roles.Select(r => r.RoleName).ToList(),                            
                        };
                     

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                                1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Podany login lub hasło są niepoprawne");
            return View(loginView);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration(RegistrationViewModel registrationView, string ReturnUrl = "")
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                // Email Verification  
                string email = Membership.GetUserNameByEmail(registrationView.Email);
                if (!string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("", "Podany adres email został już użyty");
                    return View(registrationView);
                }

                string username = Methods.Methods.GetUserNameByLogin(registrationView.UserName);
                if (!string.IsNullOrEmpty(username))
                {
                    ModelState.AddModelError("", "Podany login został już użyty");
                    return View(registrationView);
                }


                //Save User Data   
                using (PatientsPortalContext dbContext = new PatientsPortalContext())
                {
                    var user = new User()
                    {
                        UserName = registrationView.UserName,
                        FirstName = registrationView.FirstName,
                        LastName = registrationView.LastName,
                        Email = registrationView.Email,
                        MobileNumber = registrationView.MobileNumber,
                        Pesel = registrationView.Pesel,
                        CreatedDate = DateTime.Now,
                        CreatedBy = registrationView.UserName,
                        BirthDate = registrationView.BirthDate,
                        Password = registrationView.Password, //Methods.PasswordEncryption.Decrypt(),
                        Gender = registrationView.Gender,
                        IsActive = true,
                        TermsApproved = registrationView.TermsApproved,
                        TermsApprovedDate = DateTime.Now
                    };


                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    if (user.Roles == null)
                    {
                        user.Roles = new List<Role>();
                    }
                    var role = dbContext.Roles.FirstOrDefault(m => m.RoleName == "Patient");
                    user.Roles.Add(role);
                    

                    dbContext.SaveChanges();

                }

                var userLog = (CustomMembershipUser)Membership.GetUser(registrationView.UserName, false);
                if (userLog != null)
                {
                    CustomSerializeModel userModel = new CustomSerializeModel()
                    {
                        UserId = userLog.UserId,
                        FirstName = userLog.FirstName,
                        LastName = userLog.LastName,
                        RoleName = userLog.Roles.Select(r => r.RoleName).ToList()
                    };

                    string userData = JsonConvert.SerializeObject(userModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                    (
                        1, registrationView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                    );

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                    Response.Cookies.Add(faCookie);
                }

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                messageRegistration = "Something Wrong!";
            }
            ModelState.AddModelError("", "Coś poszło nie tak, upewnij się, że wszystkie pola zostały wypełnione poprawnie");

            return View(registrationView);
        }

        //[HttpGet]
        //public ActionResult ActivationAccount(string id)
        //{
        //    bool statusAccount = false;
        //    using (PatientsPortalContext dbContext = new PatientsPortalContext())
        //    {
        //        var userAccount = dbContext.Users.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();

        //        if (userAccount != null)
        //        {
        //            userAccount.IsActive = true;
        //            dbContext.SaveChanges();
        //            statusAccount = true;
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Something Wrong !!";
        //        }

        //    }
        //    ViewBag.Status = statusAccount;
        //    return View();
        //}

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        //[NonAction]
        //public void VerificationEmail(string email, string activationCode)
        //{
        //    var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
        //    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

        //    var fromEmail = new MailAddress("", "Activation Account - AKKA");
        //    var toEmail = new MailAddress(email);

        //    var fromEmailPassword = "";
        //    string subject = "Activation Account !";

        //    string body = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";

        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
        //    };

        //    using (var message = new MailMessage(fromEmail, toEmail)
        //    {
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true

        //    })

        //        smtp.Send(message);

        //}


    }
}
