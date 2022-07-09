using FinanzasAppFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanzasAppFinal.BD.Mapping
{
    public class GastoMapping : IEntityTypeConfiguration<Gasto>
    {
        public void Configure(EntityTypeBuilder<Gasto> builder)
        {
            builder.ToTable("Gastos", "dbo");
            builder.HasKey(o => o.Id);
        }
    }
}
