namespace FitnessProgram.Services.BestResultService
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.ViewModels.BestResult;

    public interface IBestResultService
    {
        public AllBestResultsQueryModel GetAll(int currPage, int postPerPage, bool isAdministrator);

        public BestResultDetailsModel GetDetails(int bestresultId);

        public void AddBestResult(BestResultFormModel model);

        public void EditBestResult(int bestResultId, BestResultFormModel model);

        public BestResultFormModel CreateEditModel(int bestResultId);

        public BestResult GetBestResultById(int id);

        public void DeleteBestResult(BestResult bestResult);
    }
}
