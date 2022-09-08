namespace FitnessProgram.Models.Partners
{
    using static ModelsConstants;
    public class AllPartnersQueryModel
    {
        public IEnumerable<PartnersViewModel> Partners { get; init; }

        public const int PostPerPage = postPerPage;

        public int CurrentPage { get; init; } = initialCurrPage;

        public int MaxPage { get; init; }
    }
}
