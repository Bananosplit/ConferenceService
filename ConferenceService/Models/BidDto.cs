using System.ComponentModel.DataAnnotations;

namespace ConferenceService.Models
{
    public class BidDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Plan { get; set; }
        public int ActivityTypeId { get; set; }
    }
}
