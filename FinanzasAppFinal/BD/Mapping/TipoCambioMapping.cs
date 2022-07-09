using FinanzasAppFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanzasAppFinal.BD.Mapping
{
    public class TipoCambioMapping : IEntityTypeConfiguration<TipoCambio>
    {
        public void Configure(EntityTypeBuilder<TipoCambio> builder)
        {
            builder.ToTable("TipoCambio", "dbo");
            builder.HasKey(o => o.Id);
        }
    }
}
