using ConferenceService.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace ConferenceService.Data
{
    public class ConferenceServiceDBContext : DbContext
    {
        public DbSet<Bid> Bids { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public ConferenceServiceDBContext(DbContextOptions options) : base(options) =>
             Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureBidsEntity(modelBuilder);
            ConfigureActivityTypeEntity(modelBuilder);
        }

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
        }

        private void ConfigureActivityTypeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityType>()
                .Property(b => b.Name).IsRequired();

            modelBuilder.Entity<ActivityType>()
               .HasData(
                new ActivityType() { Id = 1, Name = "Report" },
                new ActivityType() { Id = 2, Name = "MasterClass" },
                new ActivityType() { Id = 3, Name = "Discussion" }
                );
        }
    }
}
