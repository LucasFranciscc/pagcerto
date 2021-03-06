using api.Infrastructure.Mappings;
using api.Model.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure
{
    public class PagCertoDbContext : DbContext
    {
        public PagCertoDbContext(DbContextOptions<PagCertoDbContext> options)
            : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Anticipation> Anticipations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagCertoDbContext).Assembly);

            modelBuilder.Entity<Transaction>().Map();
            modelBuilder.Entity<Installment>().Map();
            modelBuilder.Entity<Anticipation>().Map();
        }
    }
}
