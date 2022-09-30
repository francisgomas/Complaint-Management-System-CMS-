using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Hospital Name")]
        public string Name { get; set; }
        public int Status { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
