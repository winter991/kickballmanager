using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KickballManager.Models;
using KickballManager.ViewModels;

namespace KickballManager.Controllers
{
    public class TeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private int? teamID;
        // GET: Teams
        public ActionResult Index()
        {
            return View(db.Team.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);

            team.Players = db.Players.Where(p => p.TeamID == id).ToArray<Player>();
            team.Lineups = db.Lineup.Where(l => l.TeamID == id).ToArray<Lineup>();
            if (team == null)
            {
                return HttpNotFound();
            }
          
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,TeamName")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Team.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,TeamName")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Team.Find(id);
            db.Team.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AssignPlayer(int? id)
        {
            
            if (id == null || db.Team.Find(id) == null)
            {
                return RedirectToAction("Index");
            }
          
            teamID = id;
            AvailablePlayers AvailablePlayerViewModel = new AvailablePlayers();
            AvailablePlayerViewModel.Players = CreatePlayerDropDown(id);
            AvailablePlayerViewModel.TeamID = id.Value;
            AvailablePlayerViewModel.TeamName = (db.Team.Find(id.Value)).TeamName;
            return View(AvailablePlayerViewModel);
        
        }

        [HttpPost]
        public ActionResult AssignPlayer(int teamID,int playerID)
        {

            if(playerID== 0)
            {
                return RedirectToAction("Index");
            }
            Player player = db.Players.Find(playerID);
            if (player != null && teamID>0)
            {

                player.TeamID = teamID;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();

            }
            AvailablePlayers AvailablePlayerViewModel = new AvailablePlayers();
            AvailablePlayerViewModel.Players = CreatePlayerDropDown(teamID);
            AvailablePlayerViewModel.TeamID = teamID;
            AvailablePlayerViewModel.TeamName = (db.Team.Find(teamID)).TeamName;
            return RedirectToAction("AssignPlayer/" + teamID);
           
        }
        public List<Player>  CreatePlayerDropDown(int? id)
        {
            if (id == null || db.Team.Find(id) == null)
            {
                return null;
            }
            var PlayerQuery = from d in db.Players
                              where !d.TeamID.HasValue
                              orderby d.Name
                              select d;

            return  PlayerQuery.ToList<Player>();
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
