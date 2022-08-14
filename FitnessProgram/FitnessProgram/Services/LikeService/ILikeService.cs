namespace FitnessProgram.Services.LikeService
{
    using FitnessProgram.Data.Models;

    public interface ILikeService
    {
        public string GetLikesCount(string postId);

        public void LikePost(string postId, string userId);

        public void UnlikePost(string postId, string userId);

        public List<UserLikedPost> GetAllLikesForPost(string postId);
    }
}
