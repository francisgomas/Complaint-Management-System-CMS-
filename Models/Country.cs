using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get;set; }
        public int Phonecode { get; set; }
        [Display(Name="Country")]
        public string Name { get; set; }
    }
}
