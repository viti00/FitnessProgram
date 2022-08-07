namespace FitnessProgram.Services.PostServices
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Post;

    public interface IPostService
    {
        public AllPostsQueryViewModel GetAll(int currPage, int postPerPage);

        public void Create(PostFormModel model, string userId);

        public PostDetailsModel GetPostDetails(string postId);

        public PostFormModel CreateEditModel(Post post);

        public void Edit(PostFormModel model, string postId);

        public void Delete(Post post);

        public Post GetPostById(string postId);
    }
}
