using System.ComponentModel.DataAnnotations;

namespace StorisOfTheLand.Models
{
    public class Specimen
    {
        [Key]
        public int SpecimenID { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters"), Required(ErrorMessage = "Latin Name is required")]
        public string latinName { get; set; }
    }
}