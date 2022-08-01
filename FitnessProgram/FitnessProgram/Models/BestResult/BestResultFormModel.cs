namespace FitnessProgram.Models.BestResult
{
    using FitnessProgram.Data;
    using System.ComponentModel.DataAnnotations;

    public class BestResultFormModel
    {
        [Required]
        [Url]
        public string ImageUrlBefore { get; init; }

        [Required]
        [Url]
        public string ImageUrlAfter { get; init; }

        [Required]
        [StringLength(DataConstants.StoryMaxLegth, MinimumLength = DataConstants.StoryMinLegth)]
        public string Story { get; init; }
    }
}
