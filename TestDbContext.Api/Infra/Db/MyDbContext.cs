using Microsoft.EntityFrameworkCore;
using TestDbContext.Domain.DataModel;

namespace TestDbContext.Infra.Db
{
    public class MyDbContext : DbContext
    {
        private bool _migrated = false;
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            if (!_migrated)
            {
                Database.Migrate();
                _migrated = true;
            }
        }

        public DbSet<TestDataModel>? DataModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestDataModel>(dm => {
                dm.HasKey(d => new { d.Name, d.Timestamp });
                dm.Property(d => d.Property).IsRequired();
            });
        }
    }
}
