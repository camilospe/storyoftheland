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
        public string? SpecimenImagePath { get; set; }

        public string? SpecimenAudioPath { get; set; }
    }
}
