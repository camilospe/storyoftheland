﻿using System.ComponentModel.DataAnnotations;

namespace StorisOfTheLand.Models
{
    public class Specimen
    {
        [Key]
        public int SpecimenID { get; set; }

        [Required]
        public string SpecimenName { get; set; }

        [Required(ErrorMessage = "{0} cannot be blank")]
        [StringLength(5000, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 10)]
        public string SpecimenDescription { get; set; }
    }
}
