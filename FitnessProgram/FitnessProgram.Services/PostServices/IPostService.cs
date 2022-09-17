namespace FitnessProgram.Services.PostServices
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.ViewModels.Post;

    public interface IPostService
    {
        public AllPostsQueryModel GetAll(int currPage, int postPerPage, bool isAdministrator);

        public AllPostsQueryModel GetMy(string userId, int currPage, int postPerPage);

        public void Create(PostFormModel model, string userId);

        public PostDetailsModel GetPostDetails(string postId, string userId);

        public PostFormModel CreateEditModel(Post post);

        public void Edit(PostFormModel model, string postId);

        public void Delete(Post post);

        public Post GetPostById(string postId);
    }
}
