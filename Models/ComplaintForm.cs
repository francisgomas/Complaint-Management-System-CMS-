using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class ComplaintForm
    {
        [Required]
        public Guid Id { get; set; }
        [Display(Name = "Tracking Number")]
        public string? TrackingId { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Complainant Details")]
        public int ComplainantId { get; set; }
        [ForeignKey(nameof(ComplainantId))]
        public virtual ComplainantDetails? ComplainantDetails { get; set; } = null;
        [Required]
        [Display(Name = "Complaint Details")]
        public int ComplaintId { get; set; }
        [ForeignKey(nameof(ComplaintId))]
        public virtual ComplaintDetails? ComplaintDetails { get; set; } = null;
        [Required]
        [Display(Name = "Form Status")]
        public int FormStatusId { get; set; } = 1;
        [ForeignKey(nameof(FormStatusId))]
        public virtual FormStatus? FormStatus { get; set; } = null;
        [Display(Name = "Assigned To")]
        public string? AssignedToId { get; set; }
        [ForeignKey(nameof(AssignedToId))]
        public virtual ApplicationUser? ApplicationUser { get; set; } = null;
        public string? FileName { get; set; } = string.Empty;
        [NotMapped]
        public string[]? Files { get; set; }
        [StringLength(1000)]
        public string? Comments { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
