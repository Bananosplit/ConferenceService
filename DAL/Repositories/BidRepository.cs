using DAL.Context;
using Domain.Entity;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DAL.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly ConferenceServiceDBContext dBContext;

        public BidRepository(ConferenceServiceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Bid?> Get(Guid id)
        {
            try
            {
                var existBid = await dBContext.Bids
                    .FirstOrDefaultAsync(b => b.Id == id);

                return existBid;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Bid>?> GetBidsByDate(DateTime date, bool isSumbited = false)
        {
            var bids = new List<Bid>();

            try
            {
                bids = await dBContext.Bids
                    .Include(b => b.ActivityType)
                    .Where(b => b.IsSent == isSumbited)
                    .Where(b => b.SendDate > date)
                    .ToListAsync();
            }
            catch
            {
                return null;
            }

            return bids;
        }
        public async Task<Bid?> Get(int id)
        {
            var idd = Guid.NewGuid();
            try
            {
                var existBid = await dBContext.Bids
                    .Include(b => b.ActivityType)
                    .FirstOrDefaultAsync(b => b.Id == idd);

                return existBid;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Create(Bid bid)
        {
            try
            {
                await dBContext.Bids.AddAsync(bid);
                await dBContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<bool> Update(Bid bid)
        {
            try
            {
                dBContext.Bids.Update(bid);
                await dBContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<bool> Delete(Bid bid)
        {

            try
            {
                dBContext.Bids.Remove(bid);
                await dBContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
