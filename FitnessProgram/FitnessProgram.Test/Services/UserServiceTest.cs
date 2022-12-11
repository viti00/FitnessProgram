namespace FitnessProgram.Test.Services
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Services;
    using FitnessProgram.Services.UserService;
    using FitnessProgram.Test.Mocks;

    public class UserServiceTest
    {
        [Fact]
        public void GetProfilePictureShoudReturnCorrectResultWhenUserHasProfilePicture()
        {
            using var data = DatabaseMock.Instance;
            var userService = new UserService(data);
            data.Users.Add(GetUser());
            data.ProfilePhotos.Add(new ProfilePhoto { Bytes = new byte[1024], Description = "", FileExtension = "png"});
            data.SaveChanges();
            var user = data.Users.First();
            user.ProfilePictureId = data.ProfilePhotos.First().Id;

            var profilePicture = userService.GetProfilePicture(user.Id);
            Assert.NotNull(profilePicture);
            Assert.IsType<string>(profilePicture);
            Assert.NotEqual(profilePicture, AnonymousImageConstant.AnonymousImage);
        }

        [Fact]
        public void GetProfilePictureShoudReturnCorrectResultWhenUserHasNoProfilePicture()
        {
            using var data = DatabaseMock.Instance;
            var userService = new UserService(data);
            data.Users.Add(GetUser());
            data.ProfilePhotos.Add(new ProfilePhoto { Bytes = new byte[1024], Description = "", FileExtension = "png" });
            data.SaveChanges();
            var user = data.Users.First();
            user.ProfilePictureId = 2;

            var profilePicture = userService.GetProfilePicture(user.Id);
            Assert.NotNull(profilePicture);
            Assert.IsType<string>(profilePicture);
            Assert.Equal(profilePicture, AnonymousImageConstant.AnonymousImage);
        }
    }
}
