using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoriesOfTheLand.Models
{
    

    public class Media
    {
        [Key]
        public int Id { get; set; }
        
        [RegularExpression("(^(?:[^,\\s]+(\\.(?i)(jpe?g|png)))(?:,[^,\\s]+(\\.(?i)(jpe?g|png)))*$)", ErrorMessage = "Image file must be of type png, jpg, or jpeg")]
        [StringLength(254, ErrorMessage = "Image path length must be between 6 and 254", MinimumLength = 6)]
        public string SpecimenImagePath { get; set; }
        [RegularExpression("([^\\s]+(\\.(?i)(m4a|mp3))$)", ErrorMessage = "Audio file must be of type m4a or mp3")]
        [StringLength(254, ErrorMessage = "Audio path must be between 6 and 254 characters", MinimumLength = 6)]
        public string? SpecimenAudioPath { get; set; }
    }
}