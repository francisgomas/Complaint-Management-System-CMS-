using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class NursingStation
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nursing Station")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

    }
}
