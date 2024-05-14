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
    public class AguasController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: Aguas
        public ActionResult Index()
        {
            var tbAguas = db.tbAguas.Include(t => t.tbCliente);
            return View(tbAguas.ToList());
        }

        // GET: Aguas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAgua tbAgua = db.tbAguas.Find(id);
            if (tbAgua == null)
            {
                return HttpNotFound();
            }
            return View(tbAgua);
        }

        // GET: Aguas/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.tbClientes, "IdCliente", "Cedula");
            return View();
        }

        // POST: Aguas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAgua,IdCliente,PromedioConsumo,ConsumoActual,PeriodoConsumo")] tbAgua tbAgua)
        {
            if (ModelState.IsValid)
            {
                db.tbAguas.Add(tbAgua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.tbClientes, "IdCliente", "Cedula", tbAgua.IdCliente);
            return View(tbAgua);
        }

        // GET: Aguas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAgua tbAgua = db.tbAguas.Find(id);
            if (tbAgua == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.tbClientes, "IdCliente", "Cedula", tbAgua.IdCliente);
            return View(tbAgua);
        }

        // POST: Aguas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAgua,IdCliente,PromedioConsumo,ConsumoActual,PeriodoConsumo")] tbAgua tbAgua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAgua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.tbClientes, "IdCliente", "Cedula", tbAgua.IdCliente);
            return View(tbAgua);
        }

        // GET: Aguas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAgua tbAgua = db.tbAguas.Find(id);
            if (tbAgua == null)
            {
                return HttpNotFound();
            }
            return View(tbAgua);
        }

        // POST: Aguas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAgua tbAgua = db.tbAguas.Find(id);
            db.tbAguas.Remove(tbAgua);
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
