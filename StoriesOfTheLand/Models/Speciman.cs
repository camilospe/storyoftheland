using System.ComponentModel.DataAnnotations;

namespace StorisOfTheLand.Models
{
    public class Specimen : IValidatableObject
    {
        [Key]
        public int SpecimenID { get; set; }

        [Required(ErrorMessage = "Cultural Significance is required")]
        [StringLength(3500, MinimumLength = 1, ErrorMessage = "Cultural Significance must have between 1 and 3500 characters")]
        public string CulturalSignificance { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
