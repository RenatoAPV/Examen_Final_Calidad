using FinanzasAppFinal.BD;
using FinanzasAppFinal.Models;
using FinanzasAppFinal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace FinanzasAppFinal.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ICuentaRepositorio _cuentaRepositorio;
        public CuentaController(ICuentaRepositorio cuentaRepositorio)
        {
            _cuentaRepositorio = cuentaRepositorio;
        }
        public IActionResult Index()
        {
            ViewBag.Todas = _cuentaRepositorio.ObtenerCuentas();
            var soles = _cuentaRepositorio.TotalSoles();
            var dolares = _cuentaRepositorio.TotalDolares();
            ViewBag.MontoSoles = soles;
            ViewBag.MontoDolares = dolares;
            var cambio = _cuentaRepositorio.ObtenerTipoCambio();
            ViewBag.Total = soles + (dolares * 3.9m);
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cuenta cuenta)
        {
            var duplicado = _cuentaRepositorio.ContarPorNombre(cuenta);
            if (duplicado > 0)
            {
                ModelState.AddModelError("Nombre", "Cuenta ya existente");
            }
            if (cuenta.Nombre == null)
            {
                ModelState.AddModelError("NombreVacio", "Debe ingresar un nombre");
            }
            var montoNeg = cuenta.SaldoInicial;
            if (montoNeg < 0)
            {
                ModelState.AddModelError("Saldo", "El Saldo no debe ser negativo");
            }
            if (cuenta.SaldoInicial == 0)
            {
                ModelState.AddModelError("SaldoVacio", "Debe ingresar un saldo");
            }
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            _cuentaRepositorio.GuardarCuenta(cuenta);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateGasto()
        {
            ViewBag.Cuentas = _cuentaRepositorio.ObtenerCuentas();
            return View(new Gasto());
        }
        [HttpPost]
        public IActionResult CreateGasto(Gasto gasto)
        {
            var cuentaDb = _cuentaRepositorio.NombreCuentaGasto(gasto);
            cuentaDb.SaldoInicial -= gasto.MontoGasto;
            gasto.CuentaNombre = cuentaDb.Nombre;
            var montoNeg = cuentaDb.SaldoInicial - gasto.MontoGasto;
            if (cuentaDb.Categoria == "Propio" && montoNeg < 0)
            {
                ModelState.AddModelError("Exedido", "No puede exceder el saldo de la cuenta");
            }
            if (gasto.Descripcion == null)
            {
                ModelState.AddModelError("DescVacia", "Debe ingresar una descripcion");
            }
            if (gasto.MontoGasto == 0)
            {
                ModelState.AddModelError("GastoVacio", "Debe ingresar un gasto");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Cuentas = _cuentaRepositorio.ObtenerCuentas();
                return View("CreateGasto");
            }
            _cuentaRepositorio.GuardarGasto(gasto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ListGastos()
        {
            ViewBag.Gastos = _cuentaRepositorio.ObtenerGastos();
            return View();
        }
        [HttpGet]
        public IActionResult CreateIngreso()
        {
            ViewBag.Cuentas = _cuentaRepositorio.ObtenerCuentas();
            return View(new Gasto());
        }
        [HttpPost]
        public IActionResult CreateIngreso(Ingreso ingreso)
        {
            var cuentaDb = _cuentaRepositorio.NombreCuentaIngreso(ingreso);
            cuentaDb.SaldoInicial += ingreso.MontoIngreso;
            ingreso.CuentaNombre = cuentaDb.Nombre;
            var montoNeg = cuentaDb.SaldoInicial + ingreso.MontoIngreso;
            if (ingreso.Descripcion == null)
            {
                ModelState.AddModelError("DescIngVacia", "Debe ingresar una descripcion");
            }
            if (ingreso.MontoIngreso == 0)
            {
                ModelState.AddModelError("IngresoVacio", "Debe ingresar un gasto");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Cuentas = _cuentaRepositorio.ObtenerCuentas();
                return View("CreateIngreso");
            }
            _cuentaRepositorio.GuardarIngreso(ingreso);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ListIngresos()
        {
            ViewBag.Ingresos = _cuentaRepositorio.ObtenerIngresos();
            return View();
        }
    }
}