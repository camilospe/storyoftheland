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

        [RegularExpression("(^(?:[^,\\s]+(\\.(?i)(jpe?g|png)))(?:,[^,\\s]+(\\.(?i)(jpe?g|png)))*$)", ErrorMessage = "Image path must have atleast 5 characters and be of type png, jpg, or jpeg.")]
        [StringLength(254, ErrorMessage = "Image path length must be between {2} and {1}.", MinimumLength = 6)]
        public string SpecimenImagePath { get; set; }
        [RegularExpression("([^\\s]+(\\.(?i)(m4a|mp3))$)", ErrorMessage = "Image path must have atleast 5 characters and be of type mp3 or m4a.")]
        [StringLength(254, ErrorMessage = "Image path length must be between {2} and {1}.", MinimumLength = 6)]
        public string? SpecimenAudioPath { get; set; }
    }
}