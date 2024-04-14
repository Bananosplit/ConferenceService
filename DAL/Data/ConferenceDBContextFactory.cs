using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Reflection;

namespace DAL.Data
{
    public class ConferenceDBContextFactory : IDesignTimeDbContextFactory<ConferenceServiceDBContext>
    {
        public ConferenceServiceDBContext CreateDbContext(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var assembly = Assembly.Load("ConferenceService");

            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile(path, true, true)
                .AddUserSecrets(assembly);

            var config = configBuilder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection")!;
     

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.MapEnum<ActivityType>();
            var ds = dataSourceBuilder.Build();

            var options = new DbContextOptionsBuilder<ConferenceServiceDBContext>()
             .UseNpgsql(ds).Options;

            return new ConferenceServiceDBContext(options);
        }
    }
}
