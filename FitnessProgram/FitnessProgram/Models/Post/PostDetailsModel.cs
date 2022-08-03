
namespace FitnessProgram.Models.Post
{
    using FitnessProgram.Data.Models;

    public class PostDetailsModel
    {
        public string Id { get; init; }

        public string Title { get; init; }

        public string? ImageUrl { get; init; }

        public string Text { get; init; }

        public string CreatedOn { get; init; }

        public int LikesCount { get; init; }

        public virtual ICollection<Comment> Comments { get; init; }

        public string CreatorId { get; init; }
    }
}
