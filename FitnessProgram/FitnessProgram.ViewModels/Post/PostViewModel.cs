namespace FitnessProgram.ViewModels.Post
{
    public class PostViewModel
    {
        public string PostId { get; init; }

        public string Title { get; init; }

        public List<string> Photos { get; init; }

        public DateTime CreatedOn { get; init; }

        public int LikesCount { get; init; }

        public int CommentsCount { get; init; }
    }
}
