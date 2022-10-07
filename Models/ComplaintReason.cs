using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class ComplaintReason
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Complaint Reason")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public virtual Status? Status { get; set; } = null;
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Updated At")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
