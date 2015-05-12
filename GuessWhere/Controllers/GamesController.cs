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
            var game = db.Game.Include(g => g.Image).Include(g => g.Image1).Include(g => g.Image2).Include(g => g.Image3).Include(g => g.Image4).Include(g => g.Image5).Include(g => g.Image6);
            return View(game.ToList());
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
            ViewBag.IDImg1 = new SelectList(db.Image, "IDImage", "image");
            ViewBag.IDImg2 = new SelectList(db.Image, "IDImage", "image");
            ViewBag.IDImg3 = new SelectList(db.Image, "IDImage", "image");
            ViewBag.IDImg4 = new SelectList(db.Image, "IDImage", "image");
            ViewBag.IDImg5 = new SelectList(db.Image, "IDImage", "image");
            ViewBag.IDImg6 = new SelectList(db.Image, "IDImage", "image");
            ViewBag.IDImg7 = new SelectList(db.Image, "IDImage", "image");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGame,IDImg1,IDImg2,IDImg3,IDImg4,IDImg5,IDImg6,IDImg7")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDImg1 = new SelectList(db.Image, "IDImage", "image", game.IDImg1);
            ViewBag.IDImg2 = new SelectList(db.Image, "IDImage", "image", game.IDImg2);
            ViewBag.IDImg3 = new SelectList(db.Image, "IDImage", "image", game.IDImg3);
            ViewBag.IDImg4 = new SelectList(db.Image, "IDImage", "image", game.IDImg4);
            ViewBag.IDImg5 = new SelectList(db.Image, "IDImage", "image", game.IDImg5);
            ViewBag.IDImg6 = new SelectList(db.Image, "IDImage", "image", game.IDImg6);
            ViewBag.IDImg7 = new SelectList(db.Image, "IDImage", "image", game.IDImg7);
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
            ViewBag.IDImg1 = new SelectList(db.Image, "IDImage", "image", game.IDImg1);
            ViewBag.IDImg2 = new SelectList(db.Image, "IDImage", "image", game.IDImg2);
            ViewBag.IDImg3 = new SelectList(db.Image, "IDImage", "image", game.IDImg3);
            ViewBag.IDImg4 = new SelectList(db.Image, "IDImage", "image", game.IDImg4);
            ViewBag.IDImg5 = new SelectList(db.Image, "IDImage", "image", game.IDImg5);
            ViewBag.IDImg6 = new SelectList(db.Image, "IDImage", "image", game.IDImg6);
            ViewBag.IDImg7 = new SelectList(db.Image, "IDImage", "image", game.IDImg7);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDGame,IDImg1,IDImg2,IDImg3,IDImg4,IDImg5,IDImg6,IDImg7")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDImg1 = new SelectList(db.Image, "IDImage", "image", game.IDImg1);
            ViewBag.IDImg2 = new SelectList(db.Image, "IDImage", "image", game.IDImg2);
            ViewBag.IDImg3 = new SelectList(db.Image, "IDImage", "image", game.IDImg3);
            ViewBag.IDImg4 = new SelectList(db.Image, "IDImage", "image", game.IDImg4);
            ViewBag.IDImg5 = new SelectList(db.Image, "IDImage", "image", game.IDImg5);
            ViewBag.IDImg6 = new SelectList(db.Image, "IDImage", "image", game.IDImg6);
            ViewBag.IDImg7 = new SelectList(db.Image, "IDImage", "image", game.IDImg7);
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
    }
}
