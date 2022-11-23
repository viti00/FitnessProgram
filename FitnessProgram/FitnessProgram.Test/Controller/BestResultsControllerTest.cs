namespace FitnessProgram.Test.Controller
{
    using FitnessProgram.Controllers;
    using FitnessProgram.Services.BestResultService;
    using FitnessProgram.Test.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Moq;

    public class BestResultsControllerTest
    {
        [Fact]
        public void GetDetailsShoudReturnViewWithModelBestResultDetailsModelWhenThereIsPostWithGivenId()
        {
            const int testId = 5;
            using var data = DatabaseMock.Instance;
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();
            var controller = new BestResultsController(new BestResultService(data, Mock.Of<IMemoryCache>()));

            var result = controller.Details(testId);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetDetailsShoudReturnBadRequestWhenPostWithGivenIdNotExists()
        {
            const int testId = 12;
            using var data = DatabaseMock.Instance;
            var controller = new BestResultsController(new BestResultService(data, Mock.Of<IMemoryCache>()));
            data.BestResults.AddRange(GetBestResults());
            data.SaveChanges();

            var result = controller.Details(testId);

            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }


    }
}
