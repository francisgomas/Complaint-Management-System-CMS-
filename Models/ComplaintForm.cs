using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class ComplaintForm
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Tracking Number")]
        public string TrackingId { get; set; }
        [Required]
        [Display(Name = "Complainant Details")]
        public int ComplainantId { get; set; }
        [ForeignKey(nameof(ComplainantId))]
        public virtual ComplainantDetails? ComplainantDetails { get; set; } = null;
        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public virtual Status? Status { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


    }
}
