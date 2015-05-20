using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuessWhere.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity.Infrastructure;

namespace GuessWhere.Controllers
{
    public class ImagesController : Controller
    {
        private Guess_WhereEntities1 db = new Guess_WhereEntities1();

        // GET: Images
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if(userUserName == "ADMIN"){
                return View(db.Image.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if(userUserName == "ADMIN")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Image image = db.Image.Find(id);
                if (image == null)
                {
                    return HttpNotFound();
                }
                return View(image);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if(userUserName == "ADMIN")
            {
            return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( HttpPostedFileBase upload, [Bind(Include = "IDimage,image,name,hint1,hint2,info,latitude,longitude")] Image image)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        image.image = reader.ReadBytes(upload.ContentLength);
                    }
                }
               // image.IDimage = db.Image.Max(d => d.IDimage) + 1;

                if (ModelState.IsValid)
                {
                    db.Image.Add(image);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(RetryLimitExceededException /* dex */  )
             {
               //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError(  ""  ,  "Unable to save changes. Try again, and if the problem persists see your system administrator." );
             }

                return View(image);
            }
  

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if(userUserName == "ADMIN")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Image image = db.Image.Find(id);
                if (image == null)
                {
                    return HttpNotFound();
                }
                //db.Entry(image).State = EntityState.Detached;
                return View(image);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase upload, [Bind(Include = "IDimage,image,name,hint1,hint2,info,latitude,longitude")] Image image)
        {
            if(upload == null){
                var images = (db.Image.AsNoTracking().Where(x => x.IDimage == image.IDimage));
                image.image = images.FirstOrDefault(x => x.IDimage == image.IDimage).image;
            }

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        image.image = reader.ReadBytes(upload.ContentLength);
                    }
                }
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);

        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        { 
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if(userUserName == "ADMIN")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Image image = db.Image.Find(id);
                if (image == null)
                {
                    return HttpNotFound();
                }
                return View(image);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Image.Find(id);
            db.Image.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public void Show(int id)
        {
            byte[] image = db.Image.Where(x => x.IDimage == id).SingleOrDefault().image;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "image";
            Response.BinaryWrite(image);
            Response.End();
        }

    }
}
