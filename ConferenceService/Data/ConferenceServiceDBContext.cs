using ConferenceService.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace ConferenceService.Data
{
    public class ConferenceServiceDBContext : DbContext
    {
        public DbSet<Bid> Bids { get; set; }
        public ConferenceServiceDBContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            ConfigureBidsEntity(modelBuilder);

        private void ConfigureBidsEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bid>()
                .Property(b => b.UserId).IsRequired();

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

            modelBuilder.HasPostgresEnum<ActivityType>();

            modelBuilder.Entity<Bid>()
                .Property(e => e.ActivityType)
                .HasColumnType("activity_type")
                .IsRequired();

        /*    modelBuilder.Entity<Bid>()
                .Property(b => b.ActivityType)
                .HasColumnName("Activity_Type")
                .HasColumnType("activity_type")
                .IsRequired();*/
        }
    }
}
