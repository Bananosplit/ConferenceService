using System.ComponentModel.DataAnnotations;

namespace ConferenceService.Models
{
    public class BidDto
    {
        [Required(ErrorMessage = "User Id must filled")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Name must filled")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Plan { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "You must set activity")]
        public int ActivityTypeId { get; set; }
    }
}
