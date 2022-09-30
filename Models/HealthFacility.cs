using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class HealthFacility
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Health Facility Name")]
        public string Name { get; set; }
        public int Status { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get;set; } = DateTime.Now;
    }
}
