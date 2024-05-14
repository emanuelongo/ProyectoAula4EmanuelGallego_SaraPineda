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
    public class PreciosController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: Precios
        public ActionResult Index()
        {
            return View(db.tbPrecios.ToList());
        }

        // GET: Precios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPrecio tbPrecio = db.tbPrecios.Find(id);
            if (tbPrecio == null)
            {
                return HttpNotFound();
            }
            return View(tbPrecio);
        }

        // GET: Precios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Precios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrecios,Servicio,Precio")] tbPrecio tbPrecio)
        {
            if (ModelState.IsValid)
            {
                db.tbPrecios.Add(tbPrecio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbPrecio);
        }

        // GET: Precios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPrecio tbPrecio = db.tbPrecios.Find(id);
            if (tbPrecio == null)
            {
                return HttpNotFound();
            }
            return View(tbPrecio);
        }

        // POST: Precios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrecios,Servicio,Precio")] tbPrecio tbPrecio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPrecio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbPrecio);
        }

        // GET: Precios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPrecio tbPrecio = db.tbPrecios.Find(id);
            if (tbPrecio == null)
            {
                return HttpNotFound();
            }
            return View(tbPrecio);
        }

        // POST: Precios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPrecio tbPrecio = db.tbPrecios.Find(id);
            db.tbPrecios.Remove(tbPrecio);
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
