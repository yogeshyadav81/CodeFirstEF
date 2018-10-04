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
    public class PlayerController : Controller
    {
        private NhlContext db = new NhlContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Player
        public ActionResult Index()
        {
            //  var players = db.Players.Include(p => p.Team);
            
           var players = unitOfWork.PlayerRepository.GetAll(includeProperties: "Team");
           //var players1 = unitOfWork.PlayerRepository.GetAll(orderBy: q => q.OrderBy(d => d.TeamName));
           // var players2 = unitOfWork.PlayerRepository.GetAll(filter: q => q.FirstName.ToLower()=="yogesh");
            return View(players.ToList());
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Player player = db.Players.Find(id);
            Player player = unitOfWork.PlayerRepository.GetById(c=>c.Id== id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            //ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "City");
            ViewBag.TeamName = new SelectList(unitOfWork.TeamRepository.GetAll(), "TeamName", "TeamName");
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Position,TeamName")] Player player)
        {
            if (ModelState.IsValid)
            {
                //db.Players.Add(player);
                //db.SaveChanges();
                unitOfWork.PlayerRepository.Insert(player);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.TeamName = new SelectList(unitOfWork.TeamRepository.GetAll(), "TeamName", "City", player.TeamName);
            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Player player = db.Players.Find(id);
            Player player = unitOfWork.PlayerRepository.GetById(c=> c.Id==id);
            if (player == null)
            {
                return HttpNotFound();
            }
            //ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "City", player.TeamName);
            ViewBag.TeamName = new SelectList(unitOfWork.TeamRepository.GetAll(), "TeamName", "TeamName", player.TeamName);
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Position,TeamName")] Player player)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(player).State = EntityState.Modified;
                //db.SaveChanges();
                unitOfWork.PlayerRepository.Edit(player);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "City", player.TeamName);
            ViewBag.TeamName = new SelectList(unitOfWork.TeamRepository.GetAll(), "TeamName", "TeamName", player.TeamName);
            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Player player = db.Players.Find(id);
            Player player = unitOfWork.PlayerRepository.GetById(c=>c.Id== id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Player player = db.Players.Find(id);
            //db.Players.Remove(player);
            //db.SaveChanges();

            //Player player = unitOfWork.PlayerRepository.GetById(id.ToString());
            Player player = unitOfWork.PlayerRepository.GetById(c=>c.Id == id);
            unitOfWork.PlayerRepository.Delete(player);
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
