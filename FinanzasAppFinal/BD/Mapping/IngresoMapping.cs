using FinanzasAppFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanzasAppFinal.BD.Mapping
{
    public class IngresoMapping : IEntityTypeConfiguration<Ingreso>
    {
        public void Configure(EntityTypeBuilder<Ingreso> builder)
        {
            builder.ToTable("Ingresos", "dbo");
            builder.HasKey(o => o.Id);
        }
    }
}
