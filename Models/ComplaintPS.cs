using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class ComplaintPS
    {
        [Display(Name = "Tracking Number")]
        [Required]
        public string TrackingId { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Reason for escalating complaint to Permanent Secretary (PS)")]
        [StringLength(500)]
        public string Comments { get; set; } = string.Empty;
    }
}
