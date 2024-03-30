using ConferenceService.Models;

namespace ConferenceService.Core.Repositories.Interfaces
{
    public interface IActivityRepository
    {
        Task<ActivityType> Get(int id);
        Task<List<ActivityType>> GetAll();
        Task<bool> Create(string activityName);
        Task<bool> Update(ActivityType activityType);
        Task<bool> Delete(ActivityType activityType);
    }
}