namespace FitnessProgram.Areas.Admin.Models.BestResult
{
    using System.ComponentModel.DataAnnotations;
    using static FitnessProgram.Data.DataConstants;

    public class BestResultFormModel
    {
        [Required]
        [Url]
        public string ImageUrlBefore { get; init; }

        [Required]
        [Url]
        public string ImageUrlAfter { get; init; }

        [Required]
        [StringLength(BestResultConstants.StoryMaxLegth, MinimumLength = BestResultConstants.StoryMinLegth)]
        public string Story { get; init; }


    }
}
