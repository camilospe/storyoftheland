using System;
using System.ComponentModel.DataAnnotations;

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
        [MaxLength(50, ErrorMessage ="English Name maximum is 50 characters")]
        [MinLength(3, ErrorMessage ="English Name minimum is 3 characters")]
        public string EnlgishName { get; set; }


    }
}
