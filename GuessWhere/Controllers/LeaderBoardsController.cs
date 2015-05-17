using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
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
            return View(leaderBoard.ToList().OrderByDescending(l => l.score));
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
            ViewBag.IDgame = new SelectList(db.Game, "IDgame", "IDgame");
            ViewBag.IDuser = new SelectList(db.User, "IDuser", "username");
            return View();
        }

        // POST: LeaderBoards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDboard,IDuser,IDgame,username,score")] LeaderBoard leaderBoard)
        {
            try
            {
                var user = db.User.AsNoTracking().Where(x => x.username == leaderBoard.username);
                var us = user.First(x => x.username == leaderBoard.username);
            }catch
            {
                db.User.Add(new User { username = leaderBoard.username });
                db.SaveChanges();
                leaderBoard.IDuser = (db.User.First(x => x.username == leaderBoard.username)).IDuser;
                //db = new Guess_WhereEntities1();
            }
            if (ModelState.IsValid)
            {
                db.LeaderBoard.Add(leaderBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDgame = new SelectList(db.Game, "IDgame", "IDgame", leaderBoard.IDgame);
            ViewBag.IDuser = new SelectList(db.User, "IDuser", "username", leaderBoard.IDuser);
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
            ViewBag.IDgame = new SelectList(db.Game, "IDgame", "IDgame", leaderBoard.IDgame);
            ViewBag.IDuser = new SelectList(db.User, "IDuser", "username", leaderBoard.IDuser);
            return View(leaderBoard);
        }

        // POST: LeaderBoards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDboard,IDuser,IDgame,username,score")] LeaderBoard leaderBoard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaderBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDgame = new SelectList(db.Game, "IDgame", "IDgame", leaderBoard.IDgame);
            ViewBag.IDuser = new SelectList(db.User, "IDuser", "username", leaderBoard.IDuser);
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

        //
        public ActionResult GameEnd(int? id, string gamescore)
        {

            var IDgame = (db.Game.Find(id)).IDgame;
            decimal score;
            score = decimal.Parse(gamescore, CultureInfo.InvariantCulture);
            //score = (float) Math.Round((decimal)score, 3); //so we don't have wild scores with 20 digits

            ViewBag.score = score;
            ViewBag.IDgame = IDgame;
            LeaderBoard leaderboard = new LeaderBoard { IDgame = IDgame, score = score }; 

            return View(leaderboard);
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
