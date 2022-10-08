namespace FitnessProgram.ViewModels.BestResult
{
    using static ModelsConstants;
    public class AllBestResultsQueryModel
    {
        public IEnumerable<BestResultViewModel> BestResults { get; init; }

        public const int PostPerPage = postPerPage;

        public int CurrentPage { get; init; } = initialCurrPage;

        public int MaxPage { get; init; }

        public string SearchTerm { get; init; }

        public Sorting Sorting { get; set; }
    }
}
