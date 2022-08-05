namespace FitnessProgram.Models.Post
{
    public class AllPostsQueryViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; init; }

        public const int PostPerPage = 6;

        public int CurrentPage { get; init; } = 1;

        public int MaxPage { get; init; }

        public int PostCount { get; init; }
    }
}
