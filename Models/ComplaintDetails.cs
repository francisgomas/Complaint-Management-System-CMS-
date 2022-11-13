using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class ComplaintDetails
    {
        public int Id { get;set; }
        [Required]
        [Display(Name = "Date of Incident")]
        [DataType(DataType.Date)]
        public DateTime DateofIncident { get; set; }
        [Display(Name = "Health Facility")]
        public int? HealthFacilityId { get; set; }
        [ForeignKey(nameof(HealthFacilityId))]
        public virtual HealthFacility? HealthFacility { get; set; } = null;
        [Display(Name = "Hospital")]
        public int? HospitalId { get; set; }
        [ForeignKey(nameof(HospitalId))]
        public virtual Hospital? Hospital { get; set; } = null;
        [Display(Name = "Health Center")]
        public int? HealthCenterId { get; set; }
        [ForeignKey(nameof(HealthCenterId))]
        public virtual HealthCenter? HealthCenter { get; set; } = null;
        [Display(Name = "Nursing Station")]
        public int? NursingStationId { get; set; }
        [ForeignKey(nameof(NursingStationId))]
        public virtual NursingStation? NursingStation { get; set; } = null;
        [Required]
        [Display(Name = "Complaint Reason")]
        public int ComplaintReasonId { get; set; }
        [ForeignKey(nameof(ComplaintReasonId))]
        public virtual ComplaintReason? ComplaintReason { get; set; } = null;
        [Display(Name = "Health facility name")]
        [StringLength(100)]
        public string? HealthFacilityName { get; set; }
        [Display(Name = "Is this the first time you have made this complaint?")]
        [Required]
        public string ComplaintFirstTime { get; set; } = string.Empty;
        [Display(Name = "Are you lodging the complaint on behalf of someone else?")]
        [Required]
        public string ComplaintBehalf { get; set; } = String.Empty;
        [Display(Name = "If no, please give details of when, where and to whom the complaint was reported to? other efforts made to bring the issues to the attention of other grievance/redress mechanisms and whether relief, redress or other help was received:")]
        [StringLength(1000)]
        public string? ComplaintFirstTimeReason { get; set; }
        [Display(Name = "If yes, how are you related to the complainant? (family, friend, legal representative. please provide details below – name, relation & contact details. if legal representative – name & contact details of company)")]
        [StringLength(1000)]
        public string? ComplaintBehalfReason { get; set; }
        [Required]
        [Display(Name = "Complaint Explanation")]
        [StringLength(1000)]
        public string ComplaintExplanation { get; set; } = String.Empty;
        [Display(Name = "Remedy you are seeking from the ministry")]
        [StringLength(1000)]
        public string? Remedy { get; set; } = String.Empty;
    }
}
