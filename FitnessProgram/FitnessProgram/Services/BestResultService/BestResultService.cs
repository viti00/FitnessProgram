namespace FitnessProgram.Services.BestResultService
{
    using FitnessProgram.Areas.Admin.Models.BestResult;
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;

    public class BestResultService : IBestResultService
    {
        private readonly FitnessProgramDbContext context;

        public BestResultService(FitnessProgramDbContext context)
            => this.context = context;

        public void AddBestResult(BestResultFormModel model)
        {
            var bestResult = new BestResult
            {
                ImageUrlBefore = model.ImageUrlBefore,
                ImageUrlAfter = model.ImageUrlAfter,
                Story = model.Story
            };

            context.BestResults.Add(bestResult);
            context.SaveChanges();
        }

        public BestResultFormModel CreateEditModel(int bestResultId)
        {
            var bestResult = GetBestResultById(bestResultId);

            var editModel = new BestResultFormModel
            {
                ImageUrlAfter = bestResult.ImageUrlAfter,
                ImageUrlBefore = bestResult.ImageUrlBefore,
                Story = bestResult.Story
            };

            return editModel;
        }

        public void EditBestResult(int bestResultId,BestResultFormModel model)
        {
            var bestResult = GetBestResultById(bestResultId);

            bestResult.ImageUrlBefore = model.ImageUrlBefore;
            bestResult.ImageUrlAfter = model.ImageUrlAfter;
            bestResult.Story = model.Story;

            context.SaveChanges();
        }

        public BestResult GetBestResultById(int id)
            => context.BestResults.FirstOrDefault(x => x.Id == id);
    }
}
