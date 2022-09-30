using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Tracking
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Tracking Number")]
        public string TrackingId { get; set; }
    }
}
