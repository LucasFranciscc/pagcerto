using api.Model.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public static class TransactionMapping
    {
        public static void Map(this EntityTypeBuilder<Transaction> entity)
        {
            entity.ToTable(nameof(Transaction));

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.Nsu).IsRequired();
            entity.Property(t => t.TransactionDatePerformed).IsRequired();
            entity.Property(t => t.AcquirerConfirmation).IsRequired();
            entity.Property(t => t.GrossAmount).HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(t => t.NetAmount).HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(t => t.FlatRate).HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(t => t.CardFinal).IsRequired().HasMaxLength(4);
        }
    }
}
