using api.Model.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public static class AnticipationMapping
    {
        public static void Map(this EntityTypeBuilder<Anticipation> entity)
        {
            entity.ToTable(nameof(Anticipation));

            entity.HasKey(a => a.Id);

            entity.Property(a => a.Id).ValueGeneratedOnAdd();
            entity.Property(a => a.RequestAmountOfAnticipation).HasColumnType("decimal(8,2)");
            entity.Property(a => a.AnticipatedValue).HasColumnType("decimal(8,2)");

        }
    }
}
