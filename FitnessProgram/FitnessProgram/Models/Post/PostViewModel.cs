namespace FitnessProgram.Models.Post
{
    public class PostViewModel
    {
        public string PostId { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public string CreatedOn { get; init; }

        public int LikesCount { get; init; }

        public int CommentsCount { get; init; }
    }
}
