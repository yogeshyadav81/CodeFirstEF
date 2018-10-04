using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstPrimer.Entities;
using CodeFirstPrimer.Models.NHL;
using CodeFirstPrimer.Repository;
namespace CodeFirstPrimer.Controllers
{
    public class TeamController : Controller
    {
        // private NhlContext db = new NhlContext();

        UnitOfWork unitOfWork = new UnitOfWork(); 

        // GET: Team
        public ActionResult Index()
        {
            // return View(db.Teams.ToList());
            return View(unitOfWork.TeamRepository.GetAll());
        }

        // GET: Team/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Team team = db.Teams.Find(id);
            //Team team = unitOfWork.TeamRepository.GetById(id);
            Team team = unitOfWork.TeamRepository.GetById(t=>t.TeamName == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamName,City,Province")] Team team)
        {
            if (ModelState.IsValid)
            {
                //db.Teams.Add(team);
                //db.SaveChanges();
                unitOfWork.TeamRepository.Insert(team);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Team team = db.Teams.Find(id);
            //Team team = unitOfWork.TeamRepository.GetById(id);
            Team team = unitOfWork.TeamRepository.GetById(t=>t.TeamName ==id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamName,City,Province")] Team team)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(team).State = EntityState.Modified;
                //db.SaveChanges();
                unitOfWork.TeamRepository.Edit(team);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Team team = db.Teams.Find(id);
            //Team team = unitOfWork.TeamRepository.GetById(id);
            Team team = unitOfWork.TeamRepository.GetById(t=>t.TeamName ==id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            //Team team = db.Teams.Find(id);
            //db.Teams.Remove(team);
            //db.SaveChanges();
            //Player player = unitOfWork.PlayerRepository.GetById(team.TeamName);
            //unitOfWork.PlayerRepository.Delete(player);

            // Team team = unitOfWork.TeamRepository.GetById(id);
            Team team = unitOfWork.TeamRepository.GetById(t=>t.TeamName==id);
            unitOfWork.TeamRepository.Delete(team);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
