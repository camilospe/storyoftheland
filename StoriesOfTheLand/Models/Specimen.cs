using System.ComponentModel.DataAnnotations;

namespace StorisOfTheLand.Models
{
    public class Specimen
    {
        [Key]
        public int SpecimenID { get; set; }

        [Required(ErrorMessage = "Latin Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
        public string LatinName { get; set; }
        
        [Required(ErrorMessage = "Cultural Significance is required")]
        [StringLength(3500, MinimumLength = 1, ErrorMessage = "Cultural Significance must have between 1 and 3500 characters")]
        /* A required string that holds the specimen's cultural significance. This can be a long
         * paragraph or paragraphs and has only length as a constraint */
        public string CulturalSignificance { get; set; }
    }
}