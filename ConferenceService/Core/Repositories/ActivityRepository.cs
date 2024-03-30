using ConferenceService.Core.Repositories.Interfaces;
using ConferenceService.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceService.Data.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ConferenceServiceDBContext dBContext;

        public ActivityRepository(ConferenceServiceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<bool> Create(string activityName)
        {

            try
            {
                var activity = new ActivityType() { Name = activityName };
                await dBContext.ActivityTypes.AddAsync(activity);
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<bool> Delete(ActivityType activityType)
        {

            try
            {
                dBContext.ActivityTypes.Remove(activityType);
                await dBContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<bool> Update(ActivityType activityType)
        {
            try
            {
                dBContext.ActivityTypes.Update(activityType);
                await dBContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<ActivityType> Get(int id)
        {
            var activityType = new ActivityType();
            try
            {
                activityType = await dBContext.ActivityTypes.FindAsync(id);
            }
            catch
            {
                return null;
            }

            return activityType;
        }

        public async Task<List<ActivityType>> GetAll()
        {
            var activityType = new List<ActivityType>();
            try
            {
                activityType = await dBContext.ActivityTypes.ToListAsync();
            }
            catch
            {
                return null;
            }

            return activityType;
        }

       
    }
}
