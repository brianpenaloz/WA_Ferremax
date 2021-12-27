using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WA_Ferremax.Models;
using System.Configuration;

namespace WA_Ferremax.Controllers
{
    public class HomeController : Controller
    {
        readonly Producto p = new Producto();
        readonly Factura f = new Factura();
        readonly DetalleFactura df = new DetalleFactura();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Producto> lista = p.Read();


            Producto bean = new Producto();
            bean.Nombre = "Gaseosa";
            bean.Precio_unitario = 5;
            p.Create(bean);



            DateTime FechaActual = DateTime.UtcNow;
            TimeZoneInfo serverZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(FechaActual, serverZone);

            var appSettings = ConfigurationManager.AppSettings;
            int igv = Int32.Parse(appSettings["IGV"]);

            Factura beanF = new Factura();
            beanF.Nombre_sucursal = "Lima";
            beanF.Nombre_cliente = "Empresa SAC";
            beanF.Numero_Factura = "001";
            beanF.Fecha = currentDateTime;
            beanF.Subtotal = 200;
            beanF.IGV = igv;
            beanF.Total = 200 + (200*igv)/100;
            int newid = f.Create(beanF);




            DetalleFactura beanDF = new DetalleFactura();
            beanDF.IdProducto = 100;
            beanDF.IdFactura = newid;
            beanDF.Cantidad = 5;
            beanDF.Precio_Unitario = 12;
            beanDF.SubTotal = 60;
            df.Create(beanDF);






            return View(lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
