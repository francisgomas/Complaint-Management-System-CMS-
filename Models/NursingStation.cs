using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class NursingStation
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nursing Station")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public virtual Status? Status { get; set; } = null;
        [Display(Name = "Created On")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Updated On")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

    }
}
