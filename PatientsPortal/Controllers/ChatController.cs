using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PatientsPortal.Authentication;
using PatientsPortalModel;
using PatientsPortalModel.Entities;
using PusherServer;

namespace PatientsPortal.Controllers
{
    public class ChatController : Controller
    {
        private PatientsPortalContext db = new PatientsPortalContext();

        private Pusher pusher;

        public ChatController()
        {
            var options = new PusherOptions();
            options.Cluster = "eu";
            pusher = new Pusher(
                "560687",
                "c40dbf9f8e6be29b5965",
                "f4c678b3554256222dcf", options);

            db = new PatientsPortalContext();
            db.ConfigureUsername(() => User.Identity.Name);
        }

        public JsonResult AuthForChannel(string channel_name, string socket_id)
        {

            var currentUser = (User)Session["user"];

            if (channel_name.IndexOf("presence") >= 0)
            {
                var channelData = new PresenceChannelData()
                {
                    user_id = currentUser.UserId.ToString(),
                    user_info = new
                    {
                        id = currentUser.UserId,
                        name = currentUser.FirstName + ' ' + currentUser.LastName
                    },
                };

                var presenceAuth = pusher.Authenticate(channel_name, socket_id, channelData);

                return Json(presenceAuth);

            }

            if (channel_name.IndexOf(currentUser.UserId.ToString()) == -1)
            {
                return Json(new { status = "error", message = "User cannot join channel" });
            }

            var auth = pusher.Authenticate(channel_name, socket_id);

            return Json(auth);


        }

        [CustomAuthorize(Roles = "Administrator, Registration, Patient")]
        public ActionResult Index()
        {
            int userId = Int32.Parse(Membership.GetUser().ProviderUserKey.ToString());

            User user = db.Users.FirstOrDefault(u => u.UserId == userId);

            Session["user"] = user;

            var currentUser = (User)Session["user"];

            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Administrator"))
            {
                ViewBag.allUsers = db.Users.Where(u => u.UserName != currentUser.UserName).ToList();
            }

            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Registration"))
            {
                ViewBag.allUsers = db.Users.Where(u => u.UserName != currentUser.UserName
                                                       && u.Roles.Select(m => m.RoleName == "Patient").FirstOrDefault()).ToList();
            }

            if (Roles.IsUserInRole(Membership.GetUser().UserName, "Patient"))
            {
                ViewBag.allUsers = db.Users.Where(u => u.UserName != currentUser.UserName
                                                       && u.Roles.Select(m => m.RoleName == "Registration").FirstOrDefault()).ToList();
            }


            ViewBag.currentUser = currentUser;
            return View();
        }

        public JsonResult ConversationWithContact(int contact)
        {
            var currentUser = (User)Session["user"];
            var conversations = new List<Conversation>();
            using (var db = new PatientsPortalContext())
            {
                conversations = db.Conversations.
                                  Where(c => (c.ReceiverId == currentUser.UserId && c.SenderId == contact) 
                                             || (c.ReceiverId == contact && c.SenderId == currentUser.UserId))
                                  .OrderBy(c => c.CreatedDate)
                                  .ToList();
            }
            return Json(new { status = "success", data = conversations }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendMessage()
        {
            var currentUser = (User)Session["user"];
            var contact = Convert.ToInt32(Request.Form["contact"]);
            string socket_id = Request.Form["socket_id"];
            Conversation convo = new Conversation
            {
                SenderId = currentUser.UserId,
                Message = Request.Form["Message"],
                ReceiverId = contact,
                CreatedDate = DateTime.Now,
                CreatedBy = currentUser.UserName
            };
            using (var db = new PatientsPortalContext())
            {
                db.Conversations.Add(convo);
                db.SaveChanges();
            }
            var conversationChannel = getConvoChannel(currentUser.UserId, contact);
            pusher.TriggerAsync(
              conversationChannel,
              "new_message",
              convo,
              new TriggerOptions() { SocketId = socket_id });
            return Json(convo);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Administrator, Registration, Patient")]
        public JsonResult MessageDelivered(int message_id)
        {
            Conversation convo = null;
            using (var db = new PatientsPortalContext())
            {
                convo = db.Conversations.FirstOrDefault(c => c.Id == message_id);
                if (convo != null)
                {
                    convo.Status = Conversation.MessageStatus.Delivered;
                    db.Entry(convo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            string socket_id = Request.Form["socket_id"];
            var conversationChannel = getConvoChannel(convo.SenderId, convo.ReceiverId);
            pusher.TriggerAsync(
              conversationChannel,
              "message_delivered",
              convo,
              new TriggerOptions() { SocketId = socket_id });
            return Json(convo);
        }


        private String getConvoChannel(int user_id, int contact_id)
        {
            if (user_id > contact_id)
            {
                return "private-chat-" + contact_id + "-" + user_id;
            }
            return "private-chat-" + user_id + "-" + contact_id;
        }
   
    }
}
