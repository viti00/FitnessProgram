namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static FitnessProgram.Data.DataConstants;

    public class Post
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(PostConstants.TitleMaxLength)]
        public string Title { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [MaxLength(PostConstants.TextMaxLegth)]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public List<UserLikedPost> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }
    }
}
