namespace FitnessProgram.Models.BestResult
{
    public class AllBestResultsViewModel
    {
        public IEnumerable<BestResultViewModel> BestResults { get; init; }

        public const int PostPerPage = 3;

        public int CurrentPage { get; init; } = 1;

        public int MaxPage { get; init; }
    }
}
