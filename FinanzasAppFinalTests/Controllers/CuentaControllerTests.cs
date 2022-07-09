using FinanzasAppFinal.Controllers;
using FinanzasAppFinal.Models;
using FinanzasAppFinal.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzasAppFinalTests.Controllers
{
    internal class CuentaControllerTests
    {
        [Test]
        public void Index()
        {
            var mockCuenta = new Mock<ICuentaRepositorio>();
            mockCuenta.Setup(o => o.TotalSoles()).Returns(2m);
            mockCuenta.Setup(o => o.TotalDolares()).Returns(3m);
            List<Cuenta> mockCuentaList = new List<Cuenta>();
            mockCuentaList.Add(new Cuenta());
            var controller = new CuentaController(mockCuenta.Object);
            var view = controller.Index();

            Assert.IsNotNull(view);
            Assert.AreEqual(2m, mockCuenta.Object.TotalSoles());
            Assert.AreEqual(3m, mockCuenta.Object.TotalDolares());
            Assert.AreEqual(1, mockCuentaList.Count);
        }

        [Test]
        public void CreateGet()
        {
            var mockCuenta = new Mock<ICuentaRepositorio>();
            var controller = new CuentaController(mockCuenta.Object);
            var view = controller.Index();
            Assert.IsNotNull(view);
        }
        [Test]
        public void CreatePost()
        {
            var mockCuenta = new Mock<ICuentaRepositorio>();
            var controller = new CuentaController(mockCuenta.Object);
            var view = controller.Index();
            Assert.IsNotNull(view);
        }
        [Test]
        public void PostCreateCaseCorrecto()
        {
            var mockCuenta = new Mock<ICuentaRepositorio>();
            var controller = new CuentaController(mockCuenta.Object);
            var view = controller.Create(new Cuenta()
            {
                Nombre = "Cuenta de test",
                Categoria = "Credito",
                SaldoInicial = 10m,
                Moneda = "Soles",
                EsCredito = true
            });
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
        [Test]
        public void PostCreateIncorrecto()
        {
            var mockCuenta = new Mock<ICuentaRepositorio>();
            var Cuenta1 = new Cuenta();
            mockCuenta.Setup(o => o.ContarPorNombre(Cuenta1)).Returns(1);
            var controller = new CuentaController(mockCuenta.Object);
            var view = controller.Create(Cuenta1);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void PostCreateCaseIncorrecto2()
        {
            var mockCuenta = new Mock<ICuentaRepositorio>();
            var controller = new CuentaController(mockCuenta.Object);
            var view = controller.Create(new Cuenta()
            {
                Nombre = "Cuenta de test",
                Categoria = "Credito",
                SaldoInicial = 0,
                Moneda = "Soles",
                EsCredito = true
            });
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void PostCreateCaseIncorrecto3()
        {
            var mockCuenta = new Mock<ICuentaRepositorio>();
            var controller = new CuentaController(mockCuenta.Object);
            var view = controller.Create(new Cuenta()
            {
                Nombre = null,
                Categoria = "Credito",
                SaldoInicial = 10m,
                Moneda = "Soles",
                EsCredito = true
            });
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        public void CreateGastoGet()
        {
            var mockCuenta = new Mock<ICuentaRepositorio>();
            var controller = new CuentaController(mockCuenta.Object);
            var view = controller.CreateGasto();
            Assert.IsNotNull(view);
        }

        [Test]
        public void CreateGastoPostCorrecto() {
            var mockGasto = new Mock<ICuentaRepositorio>();
            var gasto = new Gasto()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Descripcion = "Prueba",
                MontoGasto = 2m,
                CuentaNombre = "Ahorros Test"
            };
            var cuenta = new Cuenta()
            {
                Nombre = "Cuenta de test",
                Categoria = "Credito",
                SaldoInicial = 10m,
                Moneda = "Soles",
                EsCredito = true
            };
            mockGasto.Setup(o => o.NombreCuentaGasto(gasto)).Returns(cuenta);
            var controller = new CuentaController(mockGasto.Object);
            var view = controller.CreateGasto(gasto);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
        [Test]
        public void CreateGastoPostIncorrecto()
        {
            var mockGasto = new Mock<ICuentaRepositorio>();
            var gasto = new Gasto()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Descripcion = "Prueba",
                MontoGasto = 100m,
                CuentaNombre = "Ahorros Test"
            };
            var cuenta = new Cuenta()
            {
                Nombre = "Ahorros Test",
                Categoria = "Propio",
                SaldoInicial = 10m,
                Moneda = "Soles",
                EsCredito = true
            };
            mockGasto.Setup(o => o.NombreCuentaGasto(gasto)).Returns(cuenta);
            var controller = new CuentaController(mockGasto.Object);
            var view = controller.CreateGasto(gasto);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CreateGastoPostIncorrecto2()
        {
            var mockGasto = new Mock<ICuentaRepositorio>();
            var gasto = new Gasto()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Descripcion = null,
                MontoGasto = 100m,
                CuentaNombre = "Ahorros Test"
            };
            var cuenta = new Cuenta()
            {
                Nombre = "Ahorros Test",
                Categoria = "Propio",
                SaldoInicial = 10m,
                Moneda = "Soles",
                EsCredito = true
            };
            mockGasto.Setup(o => o.NombreCuentaGasto(gasto)).Returns(cuenta);
            var controller = new CuentaController(mockGasto.Object);
            var view = controller.CreateGasto(gasto);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CreateGastoPostIncorrecto3()
        {
            var mockGasto = new Mock<ICuentaRepositorio>();
            var gasto = new Gasto()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Descripcion = "Prueba",
                MontoGasto = 0,
                CuentaNombre = "Ahorros Test"
            };
            var cuenta = new Cuenta()
            {
                Nombre = "Ahorros Test",
                Categoria = "Propio",
                SaldoInicial = 10m,
                Moneda = "Soles",
                EsCredito = true
            };
            mockGasto.Setup(o => o.NombreCuentaGasto(gasto)).Returns(cuenta);
            var controller = new CuentaController(mockGasto.Object);
            var view = controller.CreateGasto(gasto);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        public void CreateGastoPostExcederCredito()
        {
            var mockGasto = new Mock<ICuentaRepositorio>();
            var gasto = new Gasto()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Descripcion = "Prueba",
                MontoGasto = 100m,
                CuentaNombre = "Ahorros Test"
            };
            var cuenta = new Cuenta()
            {
                Nombre = "Ahorros Test",
                Categoria = "Credito",
                SaldoInicial = 10m,
                Moneda = "Soles",
                EsCredito = true
            };
            mockGasto.Setup(o => o.NombreCuentaGasto(gasto)).Returns(cuenta);
            var controller = new CuentaController(mockGasto.Object);
            var view = controller.CreateGasto(gasto);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void ListGastos()
        {
            var mockGastos = new Mock<ICuentaRepositorio>();
            List<Gasto> mockGastoList = new List<Gasto>();
            mockGastoList.Add(new Gasto());
            mockGastos.Setup(o => o.ObtenerGastos()).Returns(mockGastoList);
            var controller = new CuentaController(mockGastos.Object);
            var view = controller.ListGastos();
            Assert.IsNotNull(view);
            Assert.AreEqual(1, mockGastoList.Count);
        }
        [Test]
        public void CreateIngresoGet()
        {
            var mockIngreso = new Mock<ICuentaRepositorio>();
            var controller = new CuentaController(mockIngreso.Object);
            var view = controller.CreateIngreso();
            Assert.IsNotNull(view);
        }
        [Test]
        public void CreateIngresoPost()
        {
            var mockIngreso = new Mock<ICuentaRepositorio>();
            Ingreso ingreso = new Ingreso()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Descripcion = "Desc",
                MontoIngreso = 10m,
                CuentaNombre = "Test"
            };
            Cuenta cuenta = new Cuenta();
            mockIngreso.Setup(o => o.NombreCuentaIngreso(ingreso)).Returns(cuenta);
            var controller = new CuentaController(mockIngreso.Object);      
            var view = controller.CreateIngreso(ingreso);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
        [Test]
        public void CreateIngresoPostIncorrecto()
        {
            var mockIngreso = new Mock<ICuentaRepositorio>();
            Ingreso ingreso = new Ingreso();
            Cuenta cuenta = new Cuenta();
            mockIngreso.Setup(o => o.NombreCuentaIngreso(ingreso)).Returns(cuenta);
            var controller = new CuentaController(mockIngreso.Object);
            var view = controller.CreateIngreso(ingreso);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CreateIngresoPostIncorrecto2()
        {
            var mockIngreso = new Mock<ICuentaRepositorio>();
            Ingreso ingreso = new Ingreso()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Descripcion = null,
                MontoIngreso = 10m,
                CuentaNombre = "Test"
            };
            Cuenta cuenta = new Cuenta();
            mockIngreso.Setup(o => o.NombreCuentaIngreso(ingreso)).Returns(cuenta);
            var controller = new CuentaController(mockIngreso.Object);
            var view = controller.CreateIngreso(ingreso);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CreateIngresoPostIncorrecto3()
        {
            var mockIngreso = new Mock<ICuentaRepositorio>();
            Ingreso ingreso = new Ingreso()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Descripcion = "Desc",
                MontoIngreso = 0,
                CuentaNombre = "Test"
            };
            Cuenta cuenta = new Cuenta();
            mockIngreso.Setup(o => o.NombreCuentaIngreso(ingreso)).Returns(cuenta);
            var controller = new CuentaController(mockIngreso.Object);
            var view = controller.CreateIngreso(ingreso);
            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ListIngresos()
        {
            var mockIngreso = new Mock<ICuentaRepositorio>();
            List<Ingreso> mockIngresoList = new List<Ingreso>();
            mockIngresoList.Add(new Ingreso());
            mockIngreso.Setup(o => o.ObtenerIngresos()).Returns(mockIngresoList);
            var controller = new CuentaController(mockIngreso.Object);
            var view = controller.ListIngresos();
            Assert.IsNotNull(view);
            Assert.AreEqual(1, mockIngresoList.Count);
        }
    }   
}
