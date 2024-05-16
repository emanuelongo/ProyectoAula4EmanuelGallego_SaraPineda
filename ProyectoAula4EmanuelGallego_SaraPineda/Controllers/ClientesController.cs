using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
                try
                {
                    if (db.tbClientes.Any(cliente => cliente.Cedula == tbCliente.Cedula))
                    {
                        // La cédula ya existe en la base de datos.
                        ModelState.AddModelError("Cedula", "Ya existe un cliente con esta cédula.");
                    }
                    else
                    {
                        // La cédula no existe en la base de datos.
                        // Puedes proceder a agregar el nuevo cliente.
                        db.tbClientes.Add(tbCliente);
                        db.SaveChanges();
                        return RedirectToAction("Create", "Aguas", new { id = tbCliente.IdCliente });
                    }


                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tbCliente).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex)
            {
                // Inspecciona la excepción interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Excepción interna: {ex.InnerException.Message}");
                }
                else
                {
                    Console.WriteLine("No se encontró una excepción interna.");
                }
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
            var factura = db.tbFacturas.Find(id);

            

            if (factura != null)
            {
                // Eliminar el registro
                db.tbFacturas.Remove(factura);
                db.SaveChanges();
            }

            foreach (var agua in tbCliente.tbAguas.ToList())
            {
                db.tbAguas.Remove(agua);
            }

            foreach (var energia in tbCliente.tbEnergias.ToList())
            {
                db.tbEnergias.Remove(energia);
            }

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
