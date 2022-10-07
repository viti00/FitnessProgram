namespace FitnessProgram.ViewModels.Post
{
    using static ModelsConstants;
    public class AllPostsQueryModel
    {
        public IEnumerable<PostViewModel> Posts { get; init; }

        public const int PostPerPage = postPerPage;

        public int CurrentPage { get; init; } = initialCurrPage;

        public int MaxPage { get; init; }

        public string SearchTerm { get; set; }

        public Sorting Sorting { get; set; }
    }
}
