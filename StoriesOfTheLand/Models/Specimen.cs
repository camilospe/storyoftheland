﻿using System.ComponentModel.DataAnnotations;

namespace StorisOfTheLand.Models
{
    public class Specimen : IValidatableObject
    {
        //make specimen validatable object

        [Key]
        public int SpecimenID { get; set; }


        //May have to be nullable in future implementations
        //This will allow for file validation before it is stored
        //in the server and preventing malicious content.
        //for now it will be required
        [RegularExpression(@"([^\\s]+(\\.(?i)(jpe?g|png))$)")]
        [StringLength(254, ErrorMessage = "Image path length must be between {2} and {1}.", MinimumLength = 5)]
        [Required]
        public string specimenImagePath { get; set; }

       


        public String getImagePath()
        {
            return null;
        }

        //Validate result of image path with inenurmerable
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
