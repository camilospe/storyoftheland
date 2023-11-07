using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoriesOfTheLand.Models
{
    [Keyless]
    [NotMapped]
    public class Media
    {
        public string? SpecimenImagePath { get; set; }

        public string? SpecimenAudioPath { get; set; }
    }
}
