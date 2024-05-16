using ProyectoAula4EmanuelGallego_SaraPineda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAula4EmanuelGallego_SaraPineda.Controllers
{
    public class EstadisticasEnergiaController : Controller
    {
        // GET: EstadisticasEnergia
        private EpmEntities db = new EpmEntities();

        // GET: EstadisticasEnergia
        public ActionResult Operaciones()
        {
            return View();
        }

        // Metodo para calcular el promedio de consumo actual de energia
        public ActionResult PromedioConsumo()
        {
            var promedioConsumoActual = db.tbEnergias.Average(e => e.ConsumoActual);
            ViewBag.PromedioConsumo = promedioConsumoActual;
            return View("operaciones");
        }

        // Metodo para calcular el valkor total por concepto de descuentos
        public ActionResult ValorTotalPorDescuento()
        {
            decimal precioPorKilovatio = db.tbPrecios.Where(p => p.Servicio == "energia").Select(p => p.Precio).FirstOrDefault();

            var registrosEnergia = db.tbEnergias.ToList();

            foreach (var registro in registrosEnergia)
            {
                decimal valorParcial = Convert.ToDecimal(registro.ConsumoActual) * precioPorKilovatio;
                decimal valorIncentivo = Convert.ToDecimal(registro.MetaAhorro - registro.ConsumoActual) * precioPorKilovatio;
                decimal valorPorPagarPorEnergia = valorParcial - valorIncentivo;

                // Aquí puedes hacer algo con valorPorPagarPorEnergia, como guardarlo en la base de datos o enviarlo a la vista
            }
            return View("operaciones");
        }

        // Metodo para calcular el cliente que tuvo mayor desface o diferencia del consumo de energía con respecto a la meta de ahorro.
        public ActionResult ClienteMayorDesfase()
        {
            var clienteMayorDesfase = db.tbEnergias
                .OrderByDescending(e => Math.Abs(e.MetaAhorro - e.ConsumoActual))
                .FirstOrDefault();

            if (clienteMayorDesfase != null)
            {
                var cliente = db.tbClientes.Find(clienteMayorDesfase.IdCliente);
                ViewBag.ClienteMayorDesfase = cliente;
            }

            return View("operaciones");
            
        }

        // Metodo para calcular estrato con mayor consumo de energia
        public ActionResult EstratoMayorConsumo()
        {
            var estratoMayorConsumo = db.tbEnergias
                .Join(db.tbClientes,
                    energia => energia.IdCliente,
                    cliente => cliente.IdCliente,
                    (energia, cliente) => new { Energia = energia, Cliente = cliente })
                .GroupBy(ec => ec.Cliente.Estrato)
                .Select(g => new { Estrato = g.Key, ConsumoTotal = g.Sum(ec => ec.Energia.ConsumoActual) })
                .OrderByDescending(g => g.ConsumoTotal)
                .FirstOrDefault();

            if (estratoMayorConsumo != null)
            {
                ViewBag.EstratoMayorConsumo = estratoMayorConsumo.Estrato;
            }
            return View("operaciones");
        }

        // Metodo para calcular estrato con menor consumo de energia
        public ActionResult EstratoMenorConsumo()
        {
            var estratoMenorConsumo = db.tbEnergias
                    .Join(db.tbClientes,
                        energia => energia.IdCliente,
                        cliente => cliente.IdCliente,
                        (energia, cliente) => new { Energia = energia, Cliente = cliente })
                    .GroupBy(ec => ec.Cliente.Estrato)
                    .Select(g => new { Estrato = g.Key, ConsumoTotal = g.Sum(ec => ec.Energia.ConsumoActual) })
                    .OrderBy(g => g.ConsumoTotal)
                    .FirstOrDefault();

            if (estratoMenorConsumo != null)
            {
                ViewBag.EstratoMenorConsumo = estratoMenorConsumo.Estrato;
            }

            return View("operaciones");
        }

        // Metodo para calcular el total a pagar a epm de todos los clientes
        public ActionResult TotalPagoEpm()
        {
            var totalPagoEpm = db.tbFacturas.Sum(f => f.PagoTotal);
            ViewBag.TotalPagoEpm = totalPagoEpm;

            return View("operaciones");
        }
    }
}