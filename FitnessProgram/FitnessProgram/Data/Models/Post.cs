namespace FitnessProgram.Data.Models
{
    using Microsoft.AspNetCore.Mvc;
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

        public virtual ICollection<PostPhoto> Photos { get; set; } = new HashSet<PostPhoto>();

        [Required]
        [MaxLength(PostConstants.TextMaxLegth)]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<UserLikedPost> Likes { get; set; } = new HashSet<UserLikedPost>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }
    }
}
