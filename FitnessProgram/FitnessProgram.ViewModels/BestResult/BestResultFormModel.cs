namespace FitnessProgram.ViewModels.BestResult
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static FitnessProgram.Global.GlobalConstants;

    public class BestResultFormModel
    {
        [FromForm]
        [NotMapped]
        public IFormFileCollection BeforeFiles { get; set; }

        [FromForm]
        [NotMapped]
        public IFormFileCollection AfterFiles { get; set; }

        [Required]
        [StringLength(BestResultConstants.StoryMaxLegth, MinimumLength = BestResultConstants.StoryMinLegth)]
        public string Story { get; init; }


    }
}
