using FinanzasAppFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanzasAppFinal.BD.Mapping
{
    public class CuentaMapping : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuentas", "dbo");
            builder.HasKey(o => o.Id);
        }
    }
}
