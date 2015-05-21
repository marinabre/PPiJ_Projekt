using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GuessWhere.Models;
using System.Data.Entity;

namespace GuessWhere.Controllers
{
    [Authorize]
    public class AvatarController : Controller
    {
        private Guess_WhereEntities1 db = new Guess_WhereEntities1();

        // GET: Avatar
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();
            var id = (db.User.Where(x => x.username == userUserName).First()).IDuser;
            ViewBag.id = id;
            return View();
        }

        [HttpGet]
        public FileContentResult GetAvatar(int? id)
        {
            if (id == null) { return null; }

            var avatar = db.RegisteredUser
                                .Where(x => x.IDuser == id)
                                .SingleOrDefault()
                                .avatar;
            if (avatar != null)
            {
                return new FileContentResult(avatar, "img");
            }

            return null;
        }

        [HttpPost]
        public ActionResult Save(HttpPostedFileBase upload)
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();
            var id = (db.User.Where(x => x.username == userUserName).First()).IDuser;
            var registered = db.RegisteredUser.First(x => x.IDuser == id);

            if (upload != null && upload.ContentLength > 0)
            {
              using (var reader = new System.IO.BinaryReader(upload.InputStream))
              {
                registered.avatar = reader.ReadBytes(upload.ContentLength);
              }

              db.Entry(registered).State = EntityState.Modified;
              db.SaveChanges();

            }
            return RedirectToAction("Index", "Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}