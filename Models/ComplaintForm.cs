using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class ComplaintForm
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Tracking Number")]
        public string TrackingId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


    }
}
