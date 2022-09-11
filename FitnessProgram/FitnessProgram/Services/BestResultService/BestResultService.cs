namespace FitnessProgram.Services.BestResultService
{
    using FitnessProgram.Areas.Admin.Models.BestResult;
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.BestResult;
    using Microsoft.Extensions.Caching.Memory;
    using static SharedMethods;

    public class BestResultService : IBestResultService
    {
        private readonly FitnessProgramDbContext context;
        private readonly IMemoryCache cache;

        public BestResultService(FitnessProgramDbContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }


        public AllBestResultsQueryModel GetAll(int currPage, int postPerPage, bool isAdministrator)
        {
            const string bestResultCache = "BestResultCache";

            int totalPosts;

            List<BestResultViewModel> currPageBestResults;

            List<BestResult> bestResults;

            if (isAdministrator)
            {
                totalPosts = context.BestResults.Count();

                currPageBestResults = context.BestResults
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
            }
            else
            {
                bestResults = cache.Get<List<BestResult>>(bestResultCache);
                if (bestResults == null)
                {
                    bestResults = context.BestResults
                    .OrderByDescending(x => x.CreatedOn)
                    .ToList();

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                    cache.Set(bestResultCache, bestResults, cacheOptions);
                }

                totalPosts = bestResults.Count();

                currPageBestResults = bestResults
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
            }

            var maxPage = CalcMaxPage(totalPosts, postPerPage);

            currPage = GetCurrPage(currPage, maxPage);

            var result = new AllBestResultsQueryModel
            {
                BestResults = currPageBestResults,
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
