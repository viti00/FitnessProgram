
using FitnessProgram.Controllers;
using FitnessProgram.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessProgram.Test.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShoudReturnView()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
