using ConferenceService.Core.Repositories.Interfaces;
using System.Data;

namespace ConferenceService.Core
{
    public interface IUnitOfWork
    {
        IBidRepository Bids { get; }
        IActivityRepository Activity { get; }

        void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void Commit();
        void RollBack();
    }
}