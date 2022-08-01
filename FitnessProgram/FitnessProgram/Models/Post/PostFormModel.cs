namespace FitnessProgram.Models.Post
{
    using FitnessProgram.Data;
    using System.ComponentModel.DataAnnotations;

    public class PostFormModel
    {
        [Required]
        [StringLength(DataConstants.TitleMaxLength, MinimumLength = DataConstants.TitleMinLength)]
        public string Title { get; init; }

        [Url]
        [Display(Name ="Image URL")]
        public string? ImageUrl { get; init; }

        [Required]
        [StringLength(DataConstants.TextMaxLegth, MinimumLength = DataConstants.TextMinLegth)]
        public string Text { get; init; }

    }
}
