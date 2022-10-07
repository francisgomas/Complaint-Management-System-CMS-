using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class FormStatus
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Form Status")]
        public string FormStatusName { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
