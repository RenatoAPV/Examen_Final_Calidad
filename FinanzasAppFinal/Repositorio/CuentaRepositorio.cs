using FinanzasAppFinal.BD;
using FinanzasAppFinal.Models;

namespace FinanzasAppFinal.Repositorio
{
    public interface ICuentaRepositorio
    {
        public List<Cuenta> ObtenerCuentas();
        public decimal TotalSoles();
        public decimal TotalDolares();
        public decimal ObtenerTipoCambio();
        public int ContarPorNombre(Cuenta cuenta);
        public void GuardarCuenta(Cuenta cuenta);
        public Cuenta NombreCuentaGasto(Gasto gasto);
        public void GuardarGasto(Gasto gasto);
        public List<Gasto> ObtenerGastos();
        public Cuenta NombreCuentaIngreso(Ingreso ingreso);
        public void GuardarIngreso(Ingreso ingreso);
        public List<Ingreso> ObtenerIngresos();
    }
    public class CuentaRepositorio : ICuentaRepositorio
    {
        private DbEntities _dbEntities;
        public CuentaRepositorio(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }
        
        public List<Cuenta> ObtenerCuentas()
        {
            return _dbEntities.Cuentas.ToList();
        }
        public decimal TotalSoles()
        {
            var cuentas = ObtenerCuentas();
            return cuentas.Any() ? cuentas.Where(o => o.Moneda == "Soles").Sum(o => o.SaldoInicial) : 0;
        }
        public decimal ObtenerTipoCambio()
        {
            return _dbEntities.TipoCambio.First(o => o.Id == 1).Cambio;
        }

        public decimal TotalDolares()
        {
            var cuentas = ObtenerCuentas();
            return cuentas.Any() ? cuentas.Where(o => o.Moneda == "Dolares").Sum(o => o.SaldoInicial) : 0;
        }

        public int ContarPorNombre(Cuenta cuenta)
        {
            return _dbEntities.Cuentas.Where(o => o.Nombre == cuenta.Nombre).Count();
        }

        public void GuardarCuenta(Cuenta cuenta)
        {
            _dbEntities.Cuentas.Add(cuenta);
            _dbEntities.SaveChanges();
        }

        public Cuenta NombreCuentaGasto(Gasto gasto)
        {
            return _dbEntities.Cuentas.First(o => o.Id == gasto.CuentaId);
        }

        public void GuardarGasto(Gasto gasto)
        {
            _dbEntities.Gastos.Add(gasto);
            _dbEntities.SaveChanges();
        }

        public List<Gasto> ObtenerGastos()
        {
            return _dbEntities.Gastos.ToList();
        }

        public Cuenta NombreCuentaIngreso(Ingreso ingreso)
        {
            return _dbEntities.Cuentas.First(o => o.Id == ingreso.CuentaId);
        }

        public void GuardarIngreso(Ingreso ingreso)
        {
            _dbEntities.Ingresos.Add(ingreso);
            _dbEntities.SaveChanges();
        }

        public List<Ingreso> ObtenerIngresos()
        {
            return _dbEntities.Ingresos.ToList();
        }
    }
}
