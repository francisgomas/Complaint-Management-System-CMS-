using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class ComplainantDetails
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        [StringLength(50)]
        public string? MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        [ForeignKey(nameof(GenderId))]
        public virtual Gender? Gender { get; set; } = null;
        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country? Country { get; set; } = null;
        [Required]
        [Phone]
        [StringLength(10)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Residential Address")]
        public string ResidentialAddr { get; set; }
        [StringLength(100)]
        [Display(Name = "Postal Address")]
        public string? PostalAddr { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Town/City")]
        public string TownCity { get; set; }

    }
}
