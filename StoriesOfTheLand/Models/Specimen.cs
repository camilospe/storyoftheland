using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace StoriesOfTheLand.Models
{
    //validator class that test to see if there is any non-letter attributes
    //in the EnglishName
    
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

         [Required(ErrorMessage = "English Name is required")]
        //[NonLetter]
        [RegularExpression("^[a-zA-Z ]*$",ErrorMessage= "English Name should not contain numbers or special characters.")]
         [MaxLength(50, ErrorMessage = "English name is too long must be 50 characters or less")]
         [MinLength(3, ErrorMessage = "English name is too short must be a minimum of 3 characters")]
         public string EnglishName { get; set; }
    }
}