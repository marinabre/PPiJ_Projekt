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
    public class LeaderBoardsController : Controller
    {
        private Guess_WhereEntities1 db = new Guess_WhereEntities1();

        // GET: LeaderBoards
        public ActionResult Index()
        {
            var leaderBoard = db.LeaderBoard.Include(l => l.Game).Include(l => l.User);
            return View(leaderBoard.ToList());
        }

        // GET: LeaderBoards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaderBoard leaderBoard = db.LeaderBoard.Find(id);
            if (leaderBoard == null)
            {
                return HttpNotFound();
            }
            return View(leaderBoard);
        }

        // GET: LeaderBoards/Create
        public ActionResult Create()
        {
            ViewBag.IDGame = new SelectList(db.Game, "IDGame", "IDGame");
            ViewBag.IDUser = new SelectList(db.User, "IDuser", "username");
            return View();
        }

        // POST: LeaderBoards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBoard,IDUser,IDGame,username,score")] LeaderBoard leaderBoard)
        {
            if (ModelState.IsValid)
            {
                db.LeaderBoard.Add(leaderBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDGame = new SelectList(db.Game, "IDGame", "IDGame", leaderBoard.IDGame);
            ViewBag.IDUser = new SelectList(db.User, "IDuser", "username", leaderBoard.IDUser);
            return View(leaderBoard);
        }

        // GET: LeaderBoards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaderBoard leaderBoard = db.LeaderBoard.Find(id);
            if (leaderBoard == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDGame = new SelectList(db.Game, "IDGame", "IDGame", leaderBoard.IDGame);
            ViewBag.IDUser = new SelectList(db.User, "IDuser", "username", leaderBoard.IDUser);
            return View(leaderBoard);
        }

        // POST: LeaderBoards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBoard,IDUser,IDGame,username,score")] LeaderBoard leaderBoard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaderBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDGame = new SelectList(db.Game, "IDGame", "IDGame", leaderBoard.IDGame);
            ViewBag.IDUser = new SelectList(db.User, "IDuser", "username", leaderBoard.IDUser);
            return View(leaderBoard);
        }

        // GET: LeaderBoards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaderBoard leaderBoard = db.LeaderBoard.Find(id);
            if (leaderBoard == null)
            {
                return HttpNotFound();
            }
            return View(leaderBoard);
        }

        // POST: LeaderBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaderBoard leaderBoard = db.LeaderBoard.Find(id);
            db.LeaderBoard.Remove(leaderBoard);
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
