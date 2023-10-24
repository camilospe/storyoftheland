using System.ComponentModel.DataAnnotations;

namespace StoriesOfTheLand.Models
{
    public class Specimen
    {
        //make specimen validatable object

        [Key]
        public int SpecimenID { get; set; }


        //May have to be nullable in future implementations
        //This will allow for file validation before it is stored
        //in the server and preventing malicious content.
        //for now it will be required
        [RegularExpression("([^\\s]+(\\.(?i)(jpe?g|png))$)", ErrorMessage = "Image path must have atleast 5 characters and be of type png, jpg, or jpeg.")]
        [StringLength(254, ErrorMessage = "Image path length must be between {2} and {1}.", MinimumLength = 5)]
        [Required]
        public string SpecimenImagePath { get; set; }


       [Required(ErrorMessage = "Latin Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
        public string LatinName { get; set; }
    }
}
