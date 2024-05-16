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
        public ActionResult Create(int? id)
        {
            ViewBag.IdCliente = id;
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

                // Obtener los datos de agua y energía del cliente
                var agua = db.tbAguas.FirstOrDefault(a => a.IdCliente == tbEnergia.IdCliente);
                var energia = db.tbEnergias.FirstOrDefault(e => e.IdCliente == tbEnergia.IdCliente);

                // Obtener los precios de agua y energía
                var precioAgua = db.tbPrecios.FirstOrDefault(p => p.Servicio == "agua");
                var precioEnergia = db.tbPrecios.FirstOrDefault(p => p.Servicio == "energia");

                // Verificar que los precios existen
                if (precioAgua != null && precioEnergia != null)
                {
                    // Crear la factura
                    var factura = new tbFactura
                    {
                        IdAgua = agua.IdAgua,
                        IdEnergia = energia.IdEnergia,
                        IdPrecios = precioAgua.IdPrecios, // Asegúrate de que este es el IdPrecios correcto
                        PagoAgua = (decimal)((agua.PromedioConsumo * (double)precioAgua.Precio) + ((agua.ConsumoActual - agua.PromedioConsumo) * 2 * (double)precioAgua.Precio)), // Convertir el precio a double antes de la multiplicación
                        PagoEnergia = (decimal)((energia.ConsumoActual * (double)precioEnergia.Precio) - ((energia.MetaAhorro - energia.ConsumoActual) * (double)precioEnergia.Precio)), // Convertir el precio a double antes de la multiplicación
                        PagoTotal = (decimal)(((energia.ConsumoActual * (double)precioEnergia.Precio) - ((energia.MetaAhorro - energia.ConsumoActual) * (double)precioEnergia.Precio)) + ((agua.PromedioConsumo * (double)precioAgua.Precio) + ((agua.ConsumoActual - agua.PromedioConsumo) * (2 * (double)precioAgua.Precio))))
                    };


                    db.tbFacturas.Add(factura);
                    db.SaveChanges();
                }
                else
                {
                    // Si los precios no existen, agregar un mensaje de error al ModelState
                    ModelState.AddModelError("", "No se encontraron los precios de agua o energía.");
                }

                return RedirectToAction("Index", "tbClientes");
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
                return RedirectToAction("Index", "Clientes", new { id = tbEnergia.IdCliente });
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
