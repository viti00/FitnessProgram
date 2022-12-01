namespace FitnessProgram.Test.Services
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Services.BestResultService;
    using FitnessProgram.Test.Mocks;
    using FitnessProgram.ViewModels.BestResult;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit.Sdk;

    public class BestResultsServiceTest
    {
        [Fact]
        public void GetBestResultByIdShoudReturnCorrectBestResultWhenGivenIdExist()
        {
            const int testId = 5;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();

            var bestResult = bestResultService.GetBestResultById(testId);

            Assert.NotNull(bestResult);
            Assert.Equal(testId, bestResult.Id);
        }
        [Fact]
        public void GetBestResultByIdShoudReturnNullWhenGivenIdNotExist()
        {
            const int testId = 12;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();

            var bestResult = bestResultService.GetBestResultById(testId);

            Assert.Null(bestResult);
        }


        [Fact]
        public void BestResultsGetDetailsShoudReturnDetailsViewForGivenPost()
        {
            const int testId = 5;
            using var data = DatabaseMock.Instance;
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();
            var bestResultService = new BestResultService(data, GetMemoryCache());

            var result = bestResultService.GetDetails(testId);

            Assert.NotNull(result);
            Assert.IsType<BestResultDetailsModel>(result);
        }

        [Fact]
        public void BestResultsGetDetailsShoudReturnBadRequestWhenPostIdNotExists()
        {
            const int testId = 12;
            using var data = DatabaseMock.Instance;
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();
            var bestResultService = new BestResultService(data, GetMemoryCache());

            var result = bestResultService.GetDetails(testId);

            Assert.Null(result);
        }
        [Fact]
        public void AddBestResultShoudAddCorrectBestResultToData()
        {
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            var bestResult = new BestResultFormModel { Story = "New BestResult Added" };

            bestResultService.AddBestResult(bestResult);

            Assert.Equal(1, data.BestResults.Count());
            Assert.Equal("New BestResult Added", data.BestResults.FirstOrDefault().Story);
        }

        [Fact]
        public void CreateEditModelShoudReturnCorrectFormModelForGivenId()
        {
            const int testId = 5;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();

            var model = bestResultService.CreateEditModel(testId);

            Assert.NotNull(model);
            Assert.Equal(model.Story, data.BestResults.FirstOrDefault(x => x.Id == testId).Story);
        }

        [Fact]
        public void CreateEditModelShoudReturnNullWhenGivenIdNotExists()
        {
            const int testId = 12;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();

            var model = bestResultService.CreateEditModel(testId);

            Assert.Null(model);
        }

        [Fact]
        public void EditShoudChangeDataWhenTheGivenIdExists()
        {
            const int testId = 5;
            const string newStory = "This the new story for this post";
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();
            var oldData = bestResultService.GetBestResultById(testId).Story;
            var newData = new BestResultFormModel
            {
                Story = newStory
            };

            bestResultService.EditBestResult(testId, newData);


            Assert.NotEqual(oldData, data.BestResults.FirstOrDefault(x => x.Id == testId).Story);
            Assert.Equal(newStory, data.BestResults.FirstOrDefault(x => x.Id == testId).Story);
        }

        [Fact]
        public void DeleteShoudBeRemoveCorrectBestResultFromDatabaseWhenBestResultExists()
        {
            const int testId = 5;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();
            var expected = data.BestResults.Count();
            var bestResult = data.BestResults.FirstOrDefault(x => x.Id == testId);

            bestResultService.DeleteBestResult(bestResult);

            Assert.NotEqual(expected, data.BestResults.Count());
            Assert.False(data.BestResults.Any(x => x.Id == testId));
        }

        [Fact]
        public void DeleteShoudNotRemoveBestResulstFromDatabaseWhenBestResultNotExist()
        {
            const int testId = 12;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();
            var expected = data.BestResults.Count();
            var bestResult = data.BestResults.FirstOrDefault(x => x.Id == testId);

            bestResultService.DeleteBestResult(bestResult);

            Assert.Equal(expected, data.BestResults.Count());
        }


        [Fact]
        public void GetAllShoudReturnCorrectMaxPage()
        {
            const int currPage = 1;
            const int postPerPage = 6;
            var expected = 2;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();

            var model = bestResultService.GetAll(currPage, postPerPage, new AllBestResultsQueryModel(), false);

            Assert.NotNull(model);
            Assert.Equal(expected, model.MaxPage);
        }

        [Fact]
        public void GetAllShoudReturnCorrectMaxPageWhenThereIsNoBestResults()
        {
            const int currPage = 1;
            const int postPerPage = 6;
            var expected = 1;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());

            var model = bestResultService.GetAll(currPage, postPerPage, new AllBestResultsQueryModel(), false);

            Assert.NotNull(model);
            Assert.Equal(expected, model.MaxPage);
        }

        [Theory]
        [InlineData(1, 6)]
        [InlineData(2, 4)]
        [InlineData(3, 4)]
        [InlineData(0, 6)]
        public void GetAllShoudShowCorrectNumberOfPostsForGivenPage(int currPage, int expected)
        {
            const int postPerPage = 6;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();

            var model = bestResultService.GetAll(currPage, postPerPage, new AllBestResultsQueryModel { CurrentPage = currPage }, false);

            Assert.NotNull(model);
            Assert.Equal(expected, model.BestResults.Count());
        }

        [Fact]
        public void TheCacheIsNotAvailableForUserWithAdminRole()
        {
            const int postPerPage = 6;
            const string story = "I am the newest one";
            int expected = 2;
            var cache = GetMemoryCache();
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, cache);

            data.BestResults.Add(new BestResult { Story = "Test Story Text" });
            data.SaveChanges();

            var model = bestResultService.GetAll(1, postPerPage, new AllBestResultsQueryModel { CurrentPage = 1 }, true);
            data.BestResults.Add(new BestResult { Story = story });
            data.SaveChanges();
            model = bestResultService.GetAll(1, postPerPage, new AllBestResultsQueryModel { CurrentPage = 1 }, true);

            Assert.NotNull(model);
            Assert.IsType<AllBestResultsQueryModel>(model);
            Assert.Equal(story, model.BestResults.FirstOrDefault(x => x.Id == 2).Story);
            Assert.Equal(expected, model.BestResults.Count());
            Assert.Equal(0, cache.Count);
        }
        [Fact]
        public void TheCacheIsAvailableForNonAdminUsers()
        {
            const int postPerPage = 6;
            const string story = "I am the newest one";
            int expected = 1;
            var cache = GetMemoryCache();
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, cache);

            data.BestResults.Add(new BestResult { Story = "Test Story Text" });
            data.SaveChanges();

            var model = bestResultService.GetAll(1, postPerPage, new AllBestResultsQueryModel { CurrentPage = 1 }, false);
            data.BestResults.Add(new BestResult { Story = story });
            data.SaveChanges();
            model = bestResultService.GetAll(1, postPerPage, new AllBestResultsQueryModel { CurrentPage = 1 }, false);

            Assert.NotNull(model);
            Assert.IsType<AllBestResultsQueryModel>(model);
            Assert.Null(model.BestResults.FirstOrDefault(x => x.Id == 2));
            Assert.Equal(expected, model.BestResults.Count());
            Assert.Equal(1, cache.Count);
        }
        [Theory]
        [InlineData(1, "newest", 1)]
        [InlineData(2, "newest", 1)]
        [InlineData(1, "NEWEST", 1)]
        [InlineData(1, "NeWeSt", 1)]
        [InlineData(1, "Test", 6)]
        [InlineData(2, "Test", 4)]
        [InlineData(1, "TEST", 6)]
        [InlineData(1, "T", 6)]
        [InlineData(2, "T", 5)]
        [InlineData(1, "", 6)]
        [InlineData(2, "", 5)]
        public void GetAllShoudReturnCorrectPostsWithGivenSerchTerm(int currPage, string serchTerm, int expectedCount)
        {
            const int postPerPage = 6;
            const string story = "I am the newest one";
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.BestResults.Add(new BestResult { Story = story });
            data.SaveChanges();

            var model = bestResultService.GetAll(currPage, postPerPage, new AllBestResultsQueryModel { CurrentPage = currPage, SearchTerm = serchTerm }, false);

            Assert.IsType<AllBestResultsQueryModel>(model);
            Assert.Equal(expectedCount, model.BestResults.Count());
            Assert.True(model.BestResults.FirstOrDefault().Story.Contains(serchTerm, StringComparison.OrdinalIgnoreCase));
        }

        [Theory]
        [InlineData(1, Sorting.Default, "I am the newest one")]
        [InlineData(1, Sorting.DateAscending, "Test Story Text")]
        public void SortingShoudShowCorrectlySortedPosts(int currPage, Sorting sorting, string expectedStoryFirstElement)
        {
            const int postPerPage = 6;
            const string story = "I am the newest one";
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());
            data.BestResults.AddRange(GetBestResults());
            data.BestResults.Add(new BestResult { Story = story, CreatedOn = DateTime.Now });
            data.SaveChanges();

            var model = bestResultService
                .GetAll(currPage,
                        postPerPage,
                        new AllBestResultsQueryModel { CurrentPage = currPage, Sorting = sorting },
                        false);
            Assert.IsType<AllBestResultsQueryModel>(model);
            Assert.Equal(expectedStoryFirstElement, model.BestResults.First().Story);

        }

        [Fact]
        public void AddBestResultShoudAddPhotosFromFileSystem()
        {
            int expected = 2;
            using var data = DatabaseMock.Instance;
            var bestResultService = new BestResultService(data, GetMemoryCache());

            bestResultService.AddBestResult(new BestResultFormModel { Story = "New Story", AfterFiles = new FormFileCollection { GetFormFile("first", "first") }, BeforeFiles = new FormFileCollection {GetFormFile("second", "second") } });
            var bestResult = data.BestResults.First();

            Assert.NotNull(bestResult);
            Assert.IsType<BestResult>(bestResult);
            Assert.Equal(expected, bestResult.Photos.Count());
        }
    }
}
