using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using GuessWhere.Models;

namespace GuessWhere.Controllers
{
    public class SavedGamesController : Controller
    {
        private Guess_WhereEntities1 db = new Guess_WhereEntities1();

        //// GET: SavedGames
        //public ActionResult Index()
        //{
        //    var savedGames = db.SavedGames.Include(s => s.Game).Include(s => s.User);
        //    return View(savedGames.ToList());
        //}

        //player wanting to see all his saved games
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();
            try
            {
                var id = (db.User.Where(x => x.username == userUserName).First()).IDuser;


                var savedGames = db.SavedGames.Where(s => s.IDuser == id).Include(s => s.Game).Include(s => s.User);
                return View(savedGames.ToList());
            }
            catch
            { 
                return RedirectToAction("Play", "Games");
            }
        }

        // GET: SavedGames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedGames savedGames = db.SavedGames.Find(id);
            if (savedGames == null)
            {
                return HttpNotFound();
            }
            return View(savedGames);
        }

        // GET: SavedGames/Create
        public ActionResult Create(int idgame, int iduser, decimal score)
        {
            //if a user wants to save a game he already played

            var saved = db.SavedGames.AsNoTracking().FirstOrDefault(x => x.IDGame == idgame);
            if (saved != null && saved.IDuser == iduser) 
            { 
               return RedirectToAction("Edit", new { id = saved.IDSavedGame, score = score });
            }
           
            var savedGame = new SavedGames { IDGame = idgame, IDuser = iduser, score = score, datePlayed = DateTime.Today.Date };
            db.SavedGames.Add(savedGame);
            db.SaveChanges();
            return RedirectToAction("Index", iduser );
        }


        // POST: SavedGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IDSavedGame,IDGame,IDuser,score,datePlayed")] SavedGames savedGames)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SavedGames.Add(savedGames);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IDGame = new SelectList(db.Game, "IDgame", "IDgame", savedGames.IDGame);
        //    ViewBag.IDuser = new SelectList(db.User, "IDuser", "username", savedGames.IDuser);
        //    return View(savedGames);
        //}

        // GET: SavedGames/Edit/5


        public ActionResult Edit(int? id, decimal score)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SavedGames savedGames = db.SavedGames.Find(id);
            var iduser = savedGames.IDuser;
            if (savedGames == null)
            {
                return HttpNotFound();
            }

            savedGames.score = score;
            savedGames.datePlayed = DateTime.Today.Date;
            db.Entry(savedGames).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.id = iduser;
            return RedirectToAction("Index");
        }

        // POST: SavedGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSavedGame,IDGame,IDuser,score,datePlayed")] SavedGames savedGames)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savedGames).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDGame = new SelectList(db.Game, "IDgame", "IDgame", savedGames.IDGame);
            ViewBag.IDuser = new SelectList(db.User, "IDuser", "username", savedGames.IDuser);
            return View(savedGames);
        }

        // GET: SavedGames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedGames savedGames = db.SavedGames.Find(id);
            if (savedGames == null)
            {
                return HttpNotFound();
            }
            return View(savedGames);
        }

        // POST: SavedGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SavedGames savedGames = db.SavedGames.Find(id);
            db.SavedGames.Remove(savedGames);
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
