﻿using System.ComponentModel.DataAnnotations;

namespace StoriesOfTheLand.Models
{
    public class Sponsor
    {

        [Key]
        public int SponsorID { get; set; }

        [Required(ErrorMessage = "Sponsor Name is required")]
        [StringLength(50, ErrorMessage = "Sponsor Name length must be between {2} and {1}", MinimumLength = 1)]
        public string SponsorName { get; set;}

        [Required(ErrorMessage = "Sponsor URL is required")]
        [StringLength(100, ErrorMessage = "Sponsor Link length must be between {2} and {1}", MinimumLength = 1)]
        public string SponsorURL { get; set; }


    }
}
