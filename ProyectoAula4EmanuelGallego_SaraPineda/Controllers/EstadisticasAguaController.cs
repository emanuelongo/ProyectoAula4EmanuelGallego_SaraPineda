using ProyectoAula4EmanuelGallego_SaraPineda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAula4EmanuelGallego_SaraPineda.Controllers
{
    public class EstadisticasAguaController : Controller
    {
        // GET: EstadisticasAgua
        private EpmEntities db = new EpmEntities();

        // GET: EstadisticasAgua
        public ActionResult Operaciones()
        {
            return View();
        }

        // Metodo para calcular la cantidad de m3 de agua consumidos por encima del promedio
        public ActionResult CantidadAguaMayorAlPromedio()
        {
            var MayorAlConsumoActual = db.tbAguas.Sum(e => e.ConsumoActual) - db.tbAguas.Sum(e => e.PeriodoConsumo);
            ViewBag.CantidadMayor = MayorAlConsumoActual;
            return View("operaciones");
        }

        // Metodo para calcular el porcentaje de consumo de agua excesivo de agua por estrato
        public ActionResult PorcentajeConsumoExcesivoPorEstrato()
        {
            return View("operaciones");
        }

        // Metodo para calcular el estrato con mayor consumo de agua
        public ActionResult EstratoConMayorAhorro()
        {
            var estratoMayorAhorro = db.tbAguas
                .Join(db.tbClientes,
                    agua => agua.IdCliente,
                    cliente => cliente.IdCliente,
                    (agua, cliente) => new { Agua = agua, Cliente = cliente })
                .GroupBy(ec => ec.Cliente.Estrato)
                .Select(g => new { Estrato = g.Key, ConsumoTotal = g.Sum(ec => ec.Agua.PromedioConsumo)  - g.Sum(ec => ec.Agua.ConsumoActual) })
                .OrderByDescending(g => g.ConsumoTotal)
                .FirstOrDefault();

            if (estratoMayorAhorro != null)
            {
                ViewBag.EstratoMayorAhorro = estratoMayorAhorro.Estrato;
            }
            return View("operaciones");
        }

        // Metodo para calcular cliente con mayor consumo de agua por periodo de consumo de agua
        public ActionResult ClienteMayorConsumoPorPeriodo()
        {
            var clientesMayorConsumoPorPeriodo = db.tbAguas
                .Join(db.tbClientes,
                    agua => agua.IdCliente,
                    cliente => cliente.IdCliente,
                    (agua, cliente) => new { Agua = agua, Cliente = cliente })
                .GroupBy(ac => new { ac.Agua.PeriodoConsumo, ac.Cliente.Estrato })
                .Select(g => new
                {
                    PeriodoConsumo = g.Key.PeriodoConsumo,
                    Estrato = g.Key.Estrato,
                    ClienteMayorConsumo = g.OrderByDescending(ac => ac.Agua.ConsumoActual).FirstOrDefault().Cliente
                })
                .ToList();

            ViewBag.ClientesMayorConsumoPorPeriodo = clientesMayorConsumoPorPeriodo;

            return View("Operaciones");
        }

        // Metodo para calcular el total a pagar a ep de tods los clientes
        public ActionResult TotalPagoEpm()
        {
            var totalPagoEpm = db.tbFacturas.Sum(f => f.PagoTotal);
            ViewBag.TotalPago = totalPagoEpm;

            return View("operaciones");
        }
    }
}