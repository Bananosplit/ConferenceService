using ConferenceService.Models;

namespace ConferenceService.Utils
{
    public static class Extensions
    {
        public static Bid FillBid(this Bid bid, BidDto bidDto) 
        {
            bid.Name = bidDto?.Name;
            bid.Description = bidDto?.Description;
            bid.Plan = bidDto?.Plan;
            bid.ActivityTypeId = bidDto.ActivityTypeId;
            bid.SendDate = DateTime.UtcNow;

            return bid;
        }
    }
}
