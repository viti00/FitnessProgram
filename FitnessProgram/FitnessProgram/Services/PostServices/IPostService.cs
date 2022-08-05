namespace FitnessProgram.Services.PostServices
{
    using FitnessProgram.Models.Post;

    public interface IPostService
    {
        public AllPostsQueryViewModel GetAll(int currPage, int postPerPage);

        public void Create(PostFormModel model, string userId);

        public PostDetailsModel GetPostById(string postId);
    }
}
