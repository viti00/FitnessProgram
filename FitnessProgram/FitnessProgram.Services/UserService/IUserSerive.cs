namespace FitnessProgram.Services.UserService
{
    using FitnessProgram.Data.Models;

    public interface IUserSerive
    {
        public string GetProfilePicture(string userId);
    }
}
