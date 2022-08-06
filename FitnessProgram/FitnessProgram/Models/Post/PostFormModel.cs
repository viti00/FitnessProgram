namespace FitnessProgram.Models.Post
{
    using System.ComponentModel.DataAnnotations;
    using static FitnessProgram.Data.DataConstants;

    public class PostFormModel
    {
        [Required]
        [StringLength(PostConstants.TitleMaxLength, MinimumLength = PostConstants.TitleMinLength)]
        public string Title { get; set; }

        [Url]
        [Display(Name ="Image URL")]
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(PostConstants.TextMaxLegth, MinimumLength = PostConstants.TextMinLegth)]
        public string Text { get; set; }

    }
}
