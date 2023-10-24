using System.ComponentModel.DataAnnotations;

namespace StoriesOfTheLand.Models
{
    public class Specimen
    {
        [Key]
        public int SpecimenID { get; set; }

        [Required(ErrorMessage = "Latin Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
        public string LatinName { get; set; }

        [Required(ErrorMessage = "{0} cannot be blank")]
        [StringLength(5000, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 10)]
        public string SpecimenDescription { get; set; }
    }
}