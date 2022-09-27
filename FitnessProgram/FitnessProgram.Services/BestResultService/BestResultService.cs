namespace FitnessProgram.Services.BestResultService
{
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.ViewModels.BestResult;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Memory;
    using static SharedMethods;

    public class BestResultService : IBestResultService
    {
        private readonly FitnessProgramDbContext context;
        private readonly IMemoryCache cache;

        const string typeBefore = "Before";
        const string typeAfter = "After";

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
                    BeforePhotos = x.Photos.Where(x=> x.PhotoType == typeBefore).Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
                    AfterPhotos = x.Photos.Where(x=> x.PhotoType == typeAfter).Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
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
                    .Select(x => new BestResult
                    {
                        Id = x.Id,
                        Photos = x.Photos,
                        CreatedOn = x.CreatedOn,
                        Story = x.Story
                    })
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
                    BeforePhotos = x.Photos.Where(x => x.PhotoType == typeBefore).Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
                    AfterPhotos = x.Photos.Where(x => x.PhotoType == typeAfter).Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
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
            bestResult.Photos = GetPhotos(bestresultId);

            var model = new BestResultDetailsModel
            {
                Id = bestResult.Id,
                Photos = bestResult.Photos.Select(x=> Convert.ToBase64String(x.Bytes)).ToList(),
                CreatedOn = bestResult.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                Story = bestResult.Story
            };

            return model;
        }


        public void AddBestResult(BestResultFormModel model)
        {
            var photos = PrepareCreatePhotos(model.BeforeFiles, model.AfterFiles);

            var bestResult = new BestResult
            {
                Photos = photos,
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
                Story = bestResult.Story
            };

            return editModel;
        }

        public void EditBestResult(int bestResultId,BestResultFormModel model)
        {
            var bestResult = GetBestResultById(bestResultId);
            // get old photos
            bestResult.Photos = GetPhotos(bestResultId);

            var photos = PrepareCreatePhotos(model.BeforeFiles, model.AfterFiles);

            bestResult.Photos = photos;
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


        private List<BestResultPhoto> PrepareCreatePhotos(IFormFileCollection beforeFiles, IFormFileCollection afterFiles)
        {
            List<BestResultPhoto> photos = new List<BestResultPhoto>();
            Parallel.Invoke(() =>
            {
                CreatePhotos(beforeFiles, typeBefore, photos);
            },
            () =>
            {
                CreatePhotos(afterFiles, typeAfter, photos);
            });

            return photos;
        }

        private List<BestResultPhoto> CreatePhotos(IFormFileCollection files, string type, List<BestResultPhoto> photos)
        {
            Task.Run(async () =>
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            if (memoryStream.Length < 2097152)
                            {
                                var newphoto = new BestResultPhoto()
                                {
                                    Bytes = memoryStream.ToArray(),
                                    Description = file.FileName,
                                    FileExtension = Path.GetExtension(file.FileName),
                                    Size = file.Length,
                                    PhotoType = type
                                };
                                photos.Add(newphoto);
                            }
                        }
                    }
                }
            }).GetAwaiter()
               .GetResult();

            return photos;
        }

        private List<BestResultPhoto> GetPhotos(int bestResultId)
            => context.BestResultPhotos.Where(x => x.BestResultId == bestResultId).ToList();
    }
}
