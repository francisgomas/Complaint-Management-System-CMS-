using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CMS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; } = 1;
        [ForeignKey(nameof(StatusId))]
        public virtual Status? Status { get; set; } = null;
        [Required]
        [Display(Name = "Role Name")]
        public string RoleId { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Updated On")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

    }
}
