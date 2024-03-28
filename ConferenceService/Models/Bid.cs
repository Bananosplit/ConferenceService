﻿using System.ComponentModel.DataAnnotations;

namespace ConferenceService.Models
{
    public class Bid
    {
        
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Plan { get; set; }
        public bool? IsSent { get; set; }
        public DateTime SendDate { get; set; }

        public int ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
