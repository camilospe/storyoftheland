using System.ComponentModel.DataAnnotations;

namespace StorisOfTheLand.Models
{
    public class Specimen
    {
        //make specimena validatable object

        [Key]
        public int SpecimenID { get; set; }
        
        //add validation
        public string specimenImagePath { get; set; }

        //Validate result of image path with inenurmerable
    }
}
