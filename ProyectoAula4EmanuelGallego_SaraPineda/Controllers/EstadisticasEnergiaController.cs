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
            return View("operaciones");
        }

        // Metodo para calcular el valkor total por concepto de descuentos
        public ActionResult ValorTotalPorDescuento()
        {
            return View("operaciones");
        }

        // Metodo para calcular el ckliente que tuvo mayor desface o diferencia del consumo de energía con respecto a la meta de ahorro.
        public ActionResult ClienteMayorDesface()
        {
            return View("operaciones");
        }

        // Metodo para calcular estrato con mayor consumo de energia
        public ActionResult EstratoMayorConsumo()
        {
            return View("operaciones");
        }

        // Metodo para calcular estrato con menor consumo de energia
        public ActionResult EstratoMenorConsumo()
        {
            return View("operaciones");
        }

        // Metodo para calcular el total a pagar a ep de tods los clientes
        public ActionResult TotalPagoEpm()
        {
            return View("operaciones");
        }
    }
}