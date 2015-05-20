using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if (userUserName == "ADMIN")
            {
                var leaderBoard = db.LeaderBoard.Include(l => l.Game).Include(l => l.User);
                return View(leaderBoard.ToList());
            }
            else
            {
                return RedirectToAction("IndexCommon");
            }
        }

        // GET: LeaderBoards
        public ActionResult IndexCommon()
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

        #region Create
        //user adding himself to the leaderboard
        public ActionResult CreateReg(int idgame, int iduser, decimal score, string username)
        {
            var leaderboard = new LeaderBoard { IDgame = idgame, IDuser = iduser, score = score, username = username };
            db.LeaderBoard.Add(leaderboard);
            db.SaveChanges();
            return RedirectToAction("IndexCommon");
        }

        // GET: LeaderBoards/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if (userUserName == "ADMIN")
            {
                ViewBag.IDgame = new SelectList(db.Game, "IDgame", "IDgame");
                ViewBag.IDuser = new SelectList(db.User, "IDuser", "username");
                return View();
            }
            else
            {
                return RedirectToAction("IndexCommon");
            }
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
                //checking if username already exists
                var user = db.User.AsNoTracking().Where(x => x.username == leaderBoard.username);
                var us = user.First(x => x.username == leaderBoard.username);

                //checking if username is taken by a registered user
                var registered = db.RegisteredUser.AsNoTracking().FirstOrDefault(x => x.IDuser == us.IDuser);
                if (registered != null) 
                {
                    return RedirectToAction("GameEnd", new {id = leaderBoard.IDgame, gamescore = leaderBoard.score, first = false });
                }
                else
                {
                    leaderBoard.IDuser = us.IDuser;
                }
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
                return RedirectToAction("IndexCommon");
            }

            ViewBag.IDgame = new SelectList(db.Game, "IDgame", "IDgame", leaderBoard.IDgame);
            ViewBag.IDuser = new SelectList(db.User, "IDuser", "username", leaderBoard.IDuser);
            return View(leaderBoard);
        }

        #endregion 

        #region Edit
        // GET: LeaderBoards/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if (userUserName == "ADMIN")
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
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

        #endregion 

        #region Delete
        // GET: LeaderBoards/Delete/5
        public ActionResult Delete(int? id)
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();

            if (userUserName == "ADMIN")
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
            else
            {
                return RedirectToAction("IndexCommon");
            }
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
        #endregion

        //usual game end view
        public ActionResult GameEnd(int? id, string gamescore, bool first = true)
        {
            var userId = User.Identity.GetUserId();
            var userUserName = User.Identity.GetUserName();
            var registeredUsers = db.RegisteredUser;
            var identifikator = db.User.Where(x => x.username == userUserName).First();

            var IDgame = (db.Game.Find(id)).IDgame;
            var score = decimal.Parse(gamescore, CultureInfo.InvariantCulture);

            if (registeredUsers.FirstOrDefault(x => x.IDuser == identifikator.IDuser) != null)
            {
                return RedirectToAction("RegGameEnd", new { id = IDgame, gamescore = score, username = userUserName, userid = identifikator.IDuser });
            }
            
            ViewBag.score = score;
            ViewBag.IDgame = IDgame;

            if (!first) //in case the user is trying to use a username taken by a registered user
            {
                ViewBag.error = "That username is protected!";
            }

            var leaderboard = new LeaderBoard { IDgame = IDgame, score = score };

            return View(leaderboard);
        }

        //GameEnd View for registered users
        public ActionResult RegGameEnd(int id, decimal gamescore, string username, int userid)
        {
            ViewBag.score = gamescore;
            ViewBag.IDgame = id;
            ViewBag.username = username;
            ViewBag.userid = userid;
            var leaderboard = new LeaderBoard { IDgame = id, score = gamescore };
            return View(leaderboard);
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
