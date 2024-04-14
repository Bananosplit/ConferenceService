using DAL.Entity;

namespace App.Utils
{
    public static class ServiceValidation
    {
        public static bool ValidateBid(Bid? existBid, out string message)
        {
            message = string.Empty;

            if (existBid is null)
            {
                message = "The bid not found";
                return false;
            }
            else if (existBid.IsSent == true)
            {
                message = "The bid was sent";
                return false;
            }
            else if (string.IsNullOrEmpty(existBid.Plan))
            {
                message = "The plan must be filled";
                return false;
            }
            else if (existBid.ActivityType != ActivityType.Report)
            {
                message = "The activity type must be filled";
                return false;
            }

            return true;
        }
    }
}
