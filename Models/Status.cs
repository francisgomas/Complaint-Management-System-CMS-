using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Status
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Status Name")]
        public string Name { get; set; }
        public DateTime CreatedAt { get;set; } = DateTime.Now;

    }
}
