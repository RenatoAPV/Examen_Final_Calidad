using FinanzasAppFinal.BD.Mapping;
using FinanzasAppFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanzasAppFinal.BD
{
    public class DbEntities : DbContext
    {
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<TipoCambio> TipoCambio { get; set; }
        public DbEntities(DbContextOptions<DbEntities> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.ApplyConfiguration(new CuentaMapping());
            modelbuilder.ApplyConfiguration(new GastoMapping());
            modelbuilder.ApplyConfiguration(new IngresoMapping());
            modelbuilder.ApplyConfiguration(new TipoCambioMapping());
        }
    }
}
