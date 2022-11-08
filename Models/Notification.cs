using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public Guid? ComplaintFormId { get; set; }
        [ForeignKey(nameof(ComplaintFormId))]
        public virtual ComplaintForm? ComplaintForm { get; set; } = null;
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; } = string.Empty;
        public int? StatusId { get; set; } = 1;
        [ForeignKey(nameof(StatusId))]
        public virtual Status? Status { get; set; } = null;
        [Display(Name = "User")]
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? ApplicationUser { get; set; } = null;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
    }
}
