using System.ComponentModel.DataAnnotations;

namespace ConferenceService.Models
{
    public class BidDto
    {
        [Required(ErrorMessage = "User Id must filled")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Name must filled")]
        [StringLength(100, ErrorMessage = "Name is over 100 symbols")]
        public string? Name { get; set; }

        [StringLength(300, ErrorMessage = "Name is over 300 symbols")]
        public string? Description { get; set; }

        [StringLength(1000, ErrorMessage = "Name is over 1000 symbols")]
        public string? Plan { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must set activity")]
        public int ActivityTypeId { get; set; }
    }
}
