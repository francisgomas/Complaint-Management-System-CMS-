using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Gender
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string Name { get; set; }
    }
}
