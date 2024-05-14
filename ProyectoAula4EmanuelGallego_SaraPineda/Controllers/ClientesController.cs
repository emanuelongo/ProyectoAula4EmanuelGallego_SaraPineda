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
    public class ClientesController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.tbClientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbClientes.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCliente,Cedula,Nombre,Apellidos,Celular,Correo,Estrato")] tbCliente tbCliente)
        {
            if (ModelState.IsValid)
            {
                db.tbClientes.Add(tbCliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbCliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbClientes.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,Cedula,Nombre,Apellidos,Celular,Correo,Estrato")] tbCliente tbCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbCliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbClientes.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCliente tbCliente = db.tbClientes.Find(id);
            db.tbClientes.Remove(tbCliente);
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
