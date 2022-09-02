namespace FitnessProgram.Services.BestResultService
{
    using FitnessProgram.Areas.Admin.Models.BestResult;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.BestResult;

    public interface IBestResultService
    {
        public AllBestResultsViewModel GetAll(int currPage, int postPerPage);

        public BestResultDetailsModel GetDetails(int bestresultId);

        public void AddBestResult(BestResultFormModel model);

        public void EditBestResult(int bestResultId, BestResultFormModel model);

        public BestResultFormModel CreateEditModel(int bestResultId);

        public BestResult GetBestResultById(int id);

        public void DeleteBestResult(BestResult bestResult);
    }
}
