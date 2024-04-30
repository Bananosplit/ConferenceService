using Domain.Entity;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ConferenceServiceDBContext : DbContext
    {
        public DbSet<Bid> Bids { get; set; }
        public ConferenceServiceDBContext(DbContextOptions options) : base(options) 
            => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<ActivityType>();
            ConfigureBidsEntity(modelBuilder);
        }

        private void ConfigureBidsEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bid>()
                .Property(b => b.Id)
                .IsRequired();

            modelBuilder.Entity<Bid>()
                .Property(b => b.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<Bid>()
                .Property(b => b.Description)
                .HasColumnType("varchar(300)")
                .IsRequired();

            modelBuilder.Entity<Bid>()
                .Property(b => b.Plan)
                .HasColumnType("varchar(1000)");

            modelBuilder
                .Entity<Bid>()
                .Property(b => b.ActivityType)
                .HasColumnType("activity_type");
        }
    }
}
