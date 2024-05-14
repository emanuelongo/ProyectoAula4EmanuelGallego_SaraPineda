using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoAula4EmanuelGallego_SaraPineda.Models;

namespace ProyectoAula4EmanuelGallego_SaraPineda.Controllers
{
    public class EnergiasController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: Energias
        public ActionResult Index()
        {
            var tbEnergias = db.tbEnergias.Include(t => t.tbCliente);
            return View(tbEnergias.ToList());
        }

        // GET: Energias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergia tbEnergia = db.tbEnergias.Find(id);
            if (tbEnergia == null)
            {
                return HttpNotFound();
            }
            return View(tbEnergia);
        }

        // GET: Energias/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.tbClientes, "IdCliente", "Cedula");
            return View();
        }

        // POST: Energias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEnergia,IdCliente,MetaAhorro,ConsumoActual,PeriodoConsumo")] tbEnergia tbEnergia)
        {
            if (ModelState.IsValid)
            {
                db.tbEnergias.Add(tbEnergia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.tbClientes, "IdCliente", "Cedula", tbEnergia.IdCliente);
            return View(tbEnergia);
        }

        // GET: Energias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergia tbEnergia = db.tbEnergias.Find(id);
            if (tbEnergia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.tbClientes, "IdCliente", "Cedula", tbEnergia.IdCliente);
            return View(tbEnergia);
        }

        // POST: Energias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEnergia,IdCliente,MetaAhorro,ConsumoActual,PeriodoConsumo")] tbEnergia tbEnergia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEnergia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.tbClientes, "IdCliente", "Cedula", tbEnergia.IdCliente);
            return View(tbEnergia);
        }

        // GET: Energias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergia tbEnergia = db.tbEnergias.Find(id);
            if (tbEnergia == null)
            {
                return HttpNotFound();
            }
            return View(tbEnergia);
        }

        // POST: Energias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEnergia tbEnergia = db.tbEnergias.Find(id);
            db.tbEnergias.Remove(tbEnergia);
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
