using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuessWhere.Models;

namespace GuessWhere.Controllers
{
    public class GamesController : Controller
    {
        private Guess_WhereEntities1 db = new Guess_WhereEntities1();

        // GET: Games
        public ActionResult Index()
        {
            var game = db.Game.Include(g => g.Image)
                              .Include(g => g.Image1)
                              .Include(g => g.Image2)
                              .Include(g => g.Image3)
                              .Include(g => g.Image4)
                              .Include(g => g.Image5)
                              .Include(g => g.Image6);

            return View(game.ToList());
        }

        public ActionResult Play()
        {
            Random rnd = new Random();
            int skip = rnd.Next(0, db.Game.Count());  //random.Next range is [first,last> 
            Game game = db.Game.OrderBy(x => x.IDgame).Skip(skip).Take(1).First();

            var imgIdList = new List<int>();

            imgIdList.Add(game.Image.IDimage);
            imgIdList.Add(game.Image1.IDimage);
            imgIdList.Add(game.Image2.IDimage);
            imgIdList.Add(game.Image3.IDimage);
            imgIdList.Add(game.Image4.IDimage);
            imgIdList.Add(game.Image5.IDimage);
            imgIdList.Add(game.Image6.IDimage);
           // imgIdList.Add(game.IDgame); have to find a way where we can get the game id in it too

            ViewBag.GameID = game.IDgame;
            return View(imgIdList);
        }


        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.IDimg1 = new SelectList(db.Image, "IDimage", "name");
            ViewBag.IDimg2 = new SelectList(db.Image, "IDimage", "name");
            ViewBag.IDimg3 = new SelectList(db.Image, "IDimage", "name");
            ViewBag.IDimg4 = new SelectList(db.Image, "IDimage", "name");
            ViewBag.IDimg5 = new SelectList(db.Image, "IDimage", "name");
            ViewBag.IDimg6 = new SelectList(db.Image, "IDimage", "name");
            ViewBag.IDimg7 = new SelectList(db.Image, "IDimage", "name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDgame,IDimg1,IDimg2,IDimg3,IDimg4,IDimg5,IDimg6,IDimg7")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDimg1 = new SelectList(db.Image, "IDimage", "name", game.IDimg1);
            ViewBag.IDimg2 = new SelectList(db.Image, "IDimage", "name", game.IDimg2);
            ViewBag.IDimg3 = new SelectList(db.Image, "IDimage", "name", game.IDimg3);
            ViewBag.IDimg4 = new SelectList(db.Image, "IDimage", "name", game.IDimg4);
            ViewBag.IDimg5 = new SelectList(db.Image, "IDimage", "name", game.IDimg5);
            ViewBag.IDimg6 = new SelectList(db.Image, "IDimage", "name", game.IDimg6);
            ViewBag.IDimg7 = new SelectList(db.Image, "IDimage", "name", game.IDimg7);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            List<int> selected = new List<int>();
            ViewBag.IDimg1 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg1, selected);
            selected.Add(game.IDimg1);
            ViewBag.IDimg2 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg2, selected);
            selected.Add(game.IDimg2);
            ViewBag.IDimg3 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg3, selected);
            selected.Add(game.IDimg3);
            ViewBag.IDimg4 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg4, selected);
            selected.Add(game.IDimg4);
            ViewBag.IDimg5 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg5, selected);
            selected.Add(game.IDimg5);
            ViewBag.IDimg6 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg6, selected);
            selected.Add(game.IDimg6);
            ViewBag.IDimg7 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg7, selected);
            selected.Add(game.IDimg7);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDgame,IDimg1,IDimg2,IDimg3,IDimg4,IDimg5,IDimg6,IDimg7")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDimg1 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg1);
            ViewBag.IDimg2 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg2);
            ViewBag.IDimg3 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg3);
            ViewBag.IDimg4 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg4);
            ViewBag.IDimg5 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg5);
            ViewBag.IDimg6 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg6);
            ViewBag.IDimg7 = new SelectList(db.Image.AsNoTracking(), "IDimage", "name", game.IDimg7);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Game.Find(id);
            db.Game.Remove(game);
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

        [HttpGet]
        public ContentResult GetImage(int? imgID)
        {
            if (imgID == null) { return null; }

            var image = db.Image.Find(imgID);
            var formatted = Convert.ToBase64String(image.image);

            return Content(formatted, "img/gif");
        }

        [HttpGet]
        public ActionResult GetHint(int? imgID, int hintNum)
        {
            if (imgID == null) {
                return null;
            }

            var img = db.Image.Find(imgID);

            return Content(hintNum == 1 ? img.hint1 : img.hint2);
        }

        [HttpGet]
        public ActionResult GetInfo(int? imgID)
        {
            if (imgID == null) { return null; }

            var img = db.Image.Find(imgID);

            return Content(img.info);
        }

        [HttpGet]
        public ActionResult GetCoordinates(int? imgID)
        {
            if (imgID == null) { return null; }

            var img = db.Image.Find(imgID);

            return Content(img.latitude + "|" + img.longitude);
        }
    }
}
