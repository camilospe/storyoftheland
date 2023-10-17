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
    }
}