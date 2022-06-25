using api.Model.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public static class InstallmentMapping
    {
        public static void Map(this EntityTypeBuilder<Installment> entity)
        {
            entity.ToTable(nameof(Installment));

            entity.HasKey(x => x.Id);

            entity.Property(i => i.Id).ValueGeneratedOnAdd();
            entity.Property(i => i.GrossValue).HasColumnType("decimal(8,2)");
            entity.Property(i => i.NetValue).HasColumnType("decimal(8,2)");
        }
    }
}
