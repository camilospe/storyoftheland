using System.ComponentModel.DataAnnotations;

namespace StorisOfTheLand.Models
{
    public class Specimen : IValidatableObject
    {

        [Required(ErrorMessage = "Cultural Significance is required")]
        [StringLength(3500, MinimumLength = 1, ErrorMessage = "Cultural Significance must have between 1 and 3500 characters")]
        public string culturalSignificance { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
