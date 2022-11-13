using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Report
    {
        [Display(Name = "Total number of complaints submitted")]
        public int TotalCount { get; set; } = 0;
        [Display(Name = "Total number of complaints for hospitals")]
        public int HospitalCount { get; set; } = 0;
        [Display(Name = "Total number of complaints for nursing stations")]
        public int NursingStationCount { get; set; } = 0;
        [Display(Name = "Total number of complaints for health centers")]
        public int HealthCenterCount { get; set; } = 0;
        [Display(Name = "Total number of complaints for other health facilities")]
        public int OthersCount { get; set; } = 0;
        [Display(Name="Total number of complaints in created status")]
        public int CreatedCount { get; set; } = 0;
        [Display(Name = "Total number of complaints in created status")]
        public int InProcessingCount { get; set; } = 0;
        [Display(Name = "Total number of complaints in processing status")]
        public int ReviewedCount { get; set; } = 0;
        [Display(Name = "Total number of complaints in reviewed status")]
        public int AwaitingPSCount { get; set; } = 0;
        [Display(Name = "Total number of complaints awaiting PS response status")]
        public int ProcessedCount { get; set; } = 0;
        [Display(Name = "Total number of complaints in archived status")]
        public int ArchivedCount { get; set; } = 0;
        [Display(Name = "Total number of complaints in deleted status")]
        public int DeletedCount { get; set; } = 0;
    }
}
