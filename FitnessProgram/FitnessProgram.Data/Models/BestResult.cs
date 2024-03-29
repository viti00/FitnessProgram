﻿namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static FitnessProgram.Global.GlobalConstants;

    public class BestResult
    {
        public int Id { get; set; }

        [Required]
        public List<BestResultPhoto> Photos { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MaxLength(BestResultConstants.StoryMaxLegth)]
        public string Story { get; set; }
    }
}
