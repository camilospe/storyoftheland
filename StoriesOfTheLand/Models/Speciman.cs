using System.ComponentModel.DataAnnotations;

namespace StorisOfTheLand.Models
{
    public class Specimen
    {
        [Key]
        public int SpecimenID { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 6)]
        public string SpecimenDescription { get; set; }
    }
}
