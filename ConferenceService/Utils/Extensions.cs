using ConferenceService.Models;
using DAL.Data;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ConferenceService.Utils
{
    public static class Extensions
    {
        public static Bid FillBid(this Bid bid, BidDto bidDto)
        {
            bid.Name = bidDto?.Name;
            bid.Description = bidDto?.Description;
            bid.Plan = bidDto?.Plan;
            bid.ActivityType = (ActivityType)bidDto.ActivityTypeId;
            bid.SendDate = DateTime.UtcNow;

            return bid;
        }

        public static WebApplication MigrateDataBase(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<ConferenceServiceDBContext>();

            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                webApplication.Logger.LogInformation(ex.Message);
            }

            return webApplication;
        }

        public static IServiceCollection AddConferenceDbContext(this IServiceCollection services, IConfiguration config) 
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(config.GetConnectionString("DefaultConnection"));
            dataSourceBuilder.MapEnum<ActivityType>();
            var ds = dataSourceBuilder.Build();
            
            services.AddDbContext<ConferenceServiceDBContext>(opt =>opt.UseNpgsql(ds, d => d.MigrationsAssembly("DAL")));
            
            return services;
        }
    }
}
