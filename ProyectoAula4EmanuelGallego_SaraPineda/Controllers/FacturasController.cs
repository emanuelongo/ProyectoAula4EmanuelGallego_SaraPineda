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
    public class FacturasController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: Facturas
        public ActionResult Index()
        {
            var tbFacturas = db.tbFacturas.Include(t => t.tbAgua).Include(t => t.tbEnergia).Include(t => t.tbPrecio);
            return View(tbFacturas.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFacturas.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.IdAgua = new SelectList(db.tbAguas, "IdAgua", "IdAgua");
            ViewBag.IdEnergia = new SelectList(db.tbEnergias, "IdEnergia", "IdEnergia");
            ViewBag.IdPrecios = new SelectList(db.tbPrecios, "IdPrecios", "Servicio");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFactura,IdAgua,IdEnergia,IdPrecios,PagoAgua,PagoEnergia,PagoTotal")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.tbFacturas.Add(tbFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAgua = new SelectList(db.tbAguas, "IdAgua", "IdAgua", tbFactura.IdAgua);
            ViewBag.IdEnergia = new SelectList(db.tbEnergias, "IdEnergia", "IdEnergia", tbFactura.IdEnergia);
            ViewBag.IdPrecios = new SelectList(db.tbPrecios, "IdPrecios", "Servicio", tbFactura.IdPrecios);
            return View(tbFactura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFacturas.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAgua = new SelectList(db.tbAguas, "IdAgua", "IdAgua", tbFactura.IdAgua);
            ViewBag.IdEnergia = new SelectList(db.tbEnergias, "IdEnergia", "IdEnergia", tbFactura.IdEnergia);
            ViewBag.IdPrecios = new SelectList(db.tbPrecios, "IdPrecios", "Servicio", tbFactura.IdPrecios);
            return View(tbFactura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFactura,IdAgua,IdEnergia,IdPrecios,PagoAgua,PagoEnergia,PagoTotal")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAgua = new SelectList(db.tbAguas, "IdAgua", "IdAgua", tbFactura.IdAgua);
            ViewBag.IdEnergia = new SelectList(db.tbEnergias, "IdEnergia", "IdEnergia", tbFactura.IdEnergia);
            ViewBag.IdPrecios = new SelectList(db.tbPrecios, "IdPrecios", "Servicio", tbFactura.IdPrecios);
            return View(tbFactura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFacturas.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbFactura tbFactura = db.tbFacturas.Find(id);
            db.tbFacturas.Remove(tbFactura);
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
