using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KickballManager.Models;
using System.Data.Entity.Infrastructure;

namespace KickballManager.Controllers
{
    public class LineupDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LineupDetails
        public ActionResult Index()
        {
            return View(db.LineupDetails.ToList());
        }

        // GET: LineupDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineupDetails lineupDetails = db.LineupDetails.Find(id);
            if (lineupDetails == null)
            {
                return HttpNotFound();
            }
            return View(lineupDetails);
        }

        // GET: LineupDetails/Create
        public ActionResult Create()
        {
            CreateAvailbilePlayerDropDown();
            return View();
        }

        //TODO: Implmenet Team<> player relationship
        // until then this will return all players
        private void CreateAvailbilePlayerDropDown(int? teamid = null)
        {
            if (teamid.HasValue)
            {
                var PlayerQuery = from d in db.Players
                                  where d.TeamID == teamid
                                  orderby d.Name
                                  select d;
                ViewBag.PlayerID = new SelectList(PlayerQuery, "ID", "Name");
            }
        }

        // POST: LineupDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LineupID,PlayerID,hasKicked,hasFielded")] LineupDetails lineupDetails)
        {
            if (ModelState.IsValid)
            {
                db.LineupDetails.Add(lineupDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lineupDetails);
        }

        // GET: LineupDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineupDetails lineupDetails = db.LineupDetails.Find(id);
            if (lineupDetails == null)
            {
                return HttpNotFound();
            }
            return View(lineupDetails);
        }

        // POST: LineupDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LineupDetailID,hasKicked,hasFielded")] LineupDetails lineupDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineupDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lineupDetails);
        }

        // GET: LineupDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineupDetails lineupDetails = db.LineupDetails.Find(id);
            if (lineupDetails == null)
            {
                return HttpNotFound();
            }
            return View(lineupDetails);
        }

        // POST: LineupDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineupDetails lineupDetails = db.LineupDetails.Find(id);
            db.LineupDetails.Remove(lineupDetails);
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
