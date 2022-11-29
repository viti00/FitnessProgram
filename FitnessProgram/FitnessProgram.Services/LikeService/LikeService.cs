namespace FitnessProgram.Services.LikeService
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Data;

    public class LikeService : ILikeService
    {
        private readonly FitnessProgramDbContext context;

        public LikeService(FitnessProgramDbContext context)
        => this.context = context;

        public List<UserLikedPost> GetAllLikesForPost(string postId)
            => context.UserLikedPosts.Where(x => x.PostId == postId).ToList();

        public string GetLikesCount(string id)
        {
            var likesCount = context.Posts
                            .Where(x => x.Id == id)
                            .Select(x => x.Likes.Count())
                            .FirstOrDefault();

            return likesCount.ToString();
        }

        public void LikePost(string postId, string userId)
        {
            var post = context.Posts.FirstOrDefault(x => x.Id == postId);

            if(post != null && userId != null)
            {
                var like = new UserLikedPost
                {
                    UserId = userId,
                    PostId = postId,
                };

                context.UserLikedPosts.Add(like);
                context.SaveChanges();
            }
        }

        public void UnlikePost(string postId, string userId)
        {
            var like = context.UserLikedPosts
                .Where(x => x.PostId == postId && x.UserId == userId)
                .FirstOrDefault();

            if(like != null)
            {
                context.UserLikedPosts.Remove(like);
                context.SaveChanges();
            }
        }
    }
}
