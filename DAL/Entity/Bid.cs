namespace DAL.Entity
{
    public class Bid
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Plan { get; set; }
        public bool? IsSent { get; set; }
        public DateTime SendDate { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
