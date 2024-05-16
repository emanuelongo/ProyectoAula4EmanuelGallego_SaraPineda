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
        [HttpGet]
        public ActionResult BuscarFacturas()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarFacturas(string cedula)
        {
            // Buscar el cliente con la cédula proporcionada
            var cliente = db.tbClientes.FirstOrDefault(c => c.Cedula == cedula);

            if (cliente != null)
            {
                // Si el cliente existe, buscar las facturas asociadas a ese cliente
                var facturas = db.tbFacturas.Where(f => f.tbAgua.IdCliente == cliente.IdCliente || f.tbEnergia.IdCliente == cliente.IdCliente).ToList();

                // Almacenar las facturas en TempData
                TempData["Facturas"] = facturas;

                return RedirectToAction("DetallesFacturas");
            }
            else
            {
                // Si el cliente no existe, agregar un mensaje de error al ModelState
                ModelState.AddModelError("Cedula", "No hay una persona con dicha cédula.");
            }

            return View();
        }

        [HttpGet]
        public ActionResult DetallesFacturas()
        {
            // Recuperar las facturas de TempData
            var facturas = TempData["Facturas"] as List<tbFactura>;

            return View(facturas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesFacturas(int id)
        {
            // Buscar los gastos de agua y energía del cliente
            var gastosAgua = db.tbAguas.Where(a => a.IdCliente == id).ToList();
            var gastosEnergia = db.tbEnergias.Where(e => e.IdCliente == id).ToList();

            // Buscar los precios de los servicios de agua y energía
            var precioAgua = db.tbPrecios.FirstOrDefault(p => p.Servicio == "agua").Precio;
            var precioEnergia = db.tbPrecios.FirstOrDefault(p => p.Servicio == "energia").Precio;

            // Crear una nueva factura para cada par de gastos de agua y energía
            for (int i = 0; i < gastosAgua.Count && i < gastosEnergia.Count; i++)
            {
                tbFactura factura = new tbFactura
                {
                    IdAgua = gastosAgua[i].IdAgua,
                    IdEnergia = gastosEnergia[i].IdEnergia,
                    PagoAgua = (decimal)((gastosAgua[i].PromedioConsumo * (double)precioAgua) + ((gastosAgua[i].ConsumoActual - gastosAgua[i].PromedioConsumo) * 2 * (double)precioAgua)), // Convertir el precio a double antes de la multiplicación
                    PagoEnergia = (decimal)((gastosEnergia[i].ConsumoActual * (double)precioEnergia) - ((gastosEnergia[i].MetaAhorro - gastosEnergia[i].ConsumoActual) * (double)precioEnergia)), // Convertir el precio a double antes de la multiplicación
                    PagoTotal = (decimal)(((gastosEnergia[i].ConsumoActual * (double)precioEnergia) - ((gastosEnergia[i].MetaAhorro - gastosEnergia[i].ConsumoActual) * (double)precioEnergia)) + ((gastosAgua[i].PromedioConsumo * (double)precioAgua) + ((gastosAgua[i].ConsumoActual - gastosAgua[i].PromedioConsumo) * (2 * (double)precioAgua))))
                };

                db.tbFacturas.Add(factura);
            }

            db.SaveChanges();

            // Buscar las facturas asociadas al cliente
            var facturas = db.tbFacturas.Where(f => f.tbAgua.IdCliente == id || f.tbEnergia.IdCliente == id).ToList();


            // Pasar las facturas a la vista
            return View(facturas);
        }
    }
}
