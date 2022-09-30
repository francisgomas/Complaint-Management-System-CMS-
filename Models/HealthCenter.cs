using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class HealthCenter
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Health Center Name")]
        public string Name { get; set; }
        public int Status { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now; 
    }
}
