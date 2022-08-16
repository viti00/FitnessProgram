namespace FitnessProgram.Services.BestResultService
{
    using FitnessProgram.Areas.Admin.Models.BestResult;
    using FitnessProgram.Data.Models;

    public interface IBestResultService
    {
        public void AddBestResult(BestResultFormModel model);

        public void EditBestResult(int bestResultId, BestResultFormModel model);

        public BestResultFormModel CreateEditModel(int bestResultId);

        public BestResult GetBestResultById(int id);
    }
}
