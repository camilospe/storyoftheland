using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace StoriesOfTheLand.Models
{
    //validator class that test to see if there is any non-letter attributes
    //in the EnglishName
    public class NonLetterAttribute : ValidationAttribute
    {
        //error message for the NonLetter validator
        public NonLetterAttribute()
        {
            ErrorMessage = "English Name should not contain numbers or special characters.";
        }

        //helper method to go through the string and check if any of the characters is not a letter
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string name = value as string;
            if (string.IsNullOrWhiteSpace(name))
                return true;

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