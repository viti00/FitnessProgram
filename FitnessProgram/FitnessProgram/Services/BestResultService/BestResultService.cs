namespace FitnessProgram.Services.BestResultService
{
    using FitnessProgram.Areas.Admin.Models.BestResult;
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.BestResult;

    public class BestResultService : IBestResultService
    {
        private readonly FitnessProgramDbContext context;

        public BestResultService(FitnessProgramDbContext context)
            => this.context = context;

        public AllBestResultsViewModel GetAll(int currPage, int postPerPage)
        {
            var totalBestResults = context.BestResults.Count();

            var maxPage = (int)Math.Ceiling((double)totalBestResults / postPerPage);

            if (currPage > maxPage)
            {
                if (maxPage == 0)
                {
                    maxPage = 1;
                }
                currPage = maxPage;
            }

            var bestResults = context.BestResults
                .OrderByDescending(x => x.CreatedOn)
                .Skip((currPage - 1) * postPerPage)
                .Take(postPerPage)
                .Select(x => new BestResultViewModel
                {
                    Id = x.Id,
                    ImageUrlBefore = x.ImageUrlBefore,
                    ImageUrlAfter = x.ImageUrlAfter,
                    Story = x.Story

                })
                .ToList();

            var result = new AllBestResultsViewModel
            {
                BestResults = bestResults,
                CurrentPage = currPage,
                MaxPage = maxPage
            };

            return result;
        }

        public BestResultDetailsModel GetDetails(int bestresultId)
        {
            var bestResult = GetBestResultById(bestresultId);

            var model = new BestResultDetailsModel
            {
                Id = bestResult.Id,
                ImageUrlBefore = bestResult.ImageUrlBefore,
                ImageUrlAfter = bestResult.ImageUrlAfter,
                CreatedOn = bestResult.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                Story = bestResult.Story
            };

            return model;
        }


        public void AddBestResult(BestResultFormModel model)
        {
            var bestResult = new BestResult
            {
                ImageUrlBefore = model.ImageUrlBefore,
                ImageUrlAfter = model.ImageUrlAfter,
                CreatedOn = DateTime.Now,
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

        public void DeleteBestResult(BestResult bestResult)
        {
            context.BestResults.Remove(bestResult);
            context.SaveChanges();
        }

        public BestResult GetBestResultById(int id)
            => context.BestResults.FirstOrDefault(x => x.Id == id);

    }
}
