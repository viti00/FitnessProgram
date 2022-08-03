namespace FitnessProgram.Services.PostServices
{
    using FitnessProgram.Models.Post;

    public interface IPostService
    {
        public List<PostViewModel> GetAll();

        public void Create(PostFormModel model, string userId);

        public PostDetailsModel GetPostById(string postId);
    }
}
