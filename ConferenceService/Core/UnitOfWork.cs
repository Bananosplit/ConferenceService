using ConferenceService.Core.Repositories.Interfaces;
using ConferenceService.Data;
using ConferenceService.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ConferenceService.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ConferenceServiceDBContext dBContext;

        public IBidRepository Bids { get; private set; }
        public IActivityRepository Activity { get; private set; }

        public UnitOfWork(ConferenceServiceDBContext dBContext)
        {
            this.dBContext = dBContext;
            Bids = new BidRepository(dBContext);
            Activity = new ActivityRepository(dBContext);
        }

        public void Dispose() => dBContext.Dispose();

        public void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted) =>
            dBContext.Database.BeginTransactionAsync(isolation);

        public void Commit() => dBContext.Database.CommitTransaction();

        public void RollBack() => dBContext.Database.RollbackTransaction();
    }
}
