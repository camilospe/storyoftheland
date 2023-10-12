using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace StorisOfTheLand.Models
{
    public class NonLetterAttribute : ValidationAttribute
    {
        public NonLetterAttribute()
        {
            ErrorMessage = "English Name should not contain numbers or special characters.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true; 

            string name = value as string;
            if (string.IsNullOrWhiteSpace(name))
                return true; 

            
            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
  
    public class Specimen
    {
        [Key]
        public int SpecimenID { get; set; }


        [Required(ErrorMessage ="English Name is required")]
        [NonLetter]
        [MaxLength(50, ErrorMessage = "English name is too long must be 50 characters or less")]
        [MinLength(3, ErrorMessage = "English name is too short must be a minimum of 3 characters")]
        public string EnlgishName { get; set; }


    }
}
