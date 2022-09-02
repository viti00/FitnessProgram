namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static FitnessProgram.Data.DataConstants;

    public class BestResult
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrlBefore { get; set; }

        [Required]
        public string ImageUrlAfter { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MaxLength(BestResultConstants.StoryMaxLegth)]
        public string Story { get; set; }
    }
}
