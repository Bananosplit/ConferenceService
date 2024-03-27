using System.ComponentModel.DataAnnotations;

namespace ConferenceService.Models
{
    public enum ActivityType
    {
        Report,
        MasterClass,
        Discussion
    }

    public class Bid
    {
        
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Plan { get; set; }
        public ActivityType ActivityType { get; set; }
        public bool? IsSent { get; set; }
        public DateTime SendDate { get; set; }
    }
}
