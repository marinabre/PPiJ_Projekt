﻿using System;
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
            return View(leaderBoard.ToList());
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
        public ActionResult GameEnd(int? id, string gamescore, bool first = true)
        {

            var IDgame = (db.Game.Find(id)).IDgame;
            var score = decimal.Parse(gamescore, CultureInfo.InvariantCulture);
            
            ViewBag.score = score;
            ViewBag.IDgame = IDgame;

            if (!first) //in case the user is trying to use a username taken by a registered user
            {
                ViewBag.error = "That username is protected!";
            }

            var leaderboard = new LeaderBoard { IDgame = IDgame, score = score }; 

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
