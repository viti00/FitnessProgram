namespace FitnessProgram.Models.Post
{
    using FitnessProgram.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static FitnessProgram.Data.DataConstants;

    public class PostFormModel
    {
        [Required]
        [StringLength(PostConstants.TitleMaxLength, MinimumLength = PostConstants.TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(PostConstants.TextMaxLegth, MinimumLength = PostConstants.TextMinLegth)]
        public string Text { get; set; }


        [FromForm]
        [NotMapped]
        public IFormFileCollection? Files { get; set; }
    }
}
