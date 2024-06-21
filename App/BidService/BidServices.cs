using App.Utils;
using Domain.Entity;
using Domain.Enums;
using Domain.interfaces.Repositories;
using System.ComponentModel;
using System.Linq.Expressions;

namespace App.BidServices
{
    public class BidServices
    {

        private readonly IBidRepository bidRepo;
        public BidServices(IBidRepository bidRepo) => 
            this.bidRepo = bidRepo;

        public async Task<bool> Create(Bid bid) 
        {
            //validation
            var existBid = await bidRepo.Get(Guid.NewGuid());

            if (existBid != null)
                return false;
                //throw new ArgumentException("The Bid already EXISTS. You cant create more than one bid");

            await bidRepo.Create(bid);
            return true; 
        }

        public async Task<bool> Update(Bid bid)
        {
            //validation
            var existBid = await bidRepo.Get(Guid.NewGuid());

            if (existBid != null)
                //throw new ArgumentException("The Bid already EXISTS. You cant create more than one bid");
                return false;

            await bidRepo.Update(bid);
            return true;
        }   
        
        public async Task<bool> Delete(Guid id)
        {
            //validation
            var existBid = await bidRepo.Get(id);

            if (existBid == null)
                //throw new ArgumentException("The Bid already EXISTS. You cant create more than one bid");
                return false;

            await bidRepo.Delete(existBid);
            return true;
        }

        public async Task<bool> SendToCommission(Guid id)
        {
            Bid? existBid = await bidRepo.Get(id);

            if (ServiceValidation.ValidateBid(existBid, out string message) == false)
                return false;

            existBid.IsSent = true;
            existBid.SendDate = DateTime.UtcNow;

            bool result = await bidRepo.Update(existBid);

            return result;
        }

        public List<string> GetActivityType()
        {
            var values = new List<string>();
            var memberActivityType = typeof(ActivityType).GetFields();

            foreach (var field in memberActivityType)
            {
                var attribute = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault();

                if (attribute != null)
                    values.Add(attribute.Description);
            }

            return values;
        }
    }
}
