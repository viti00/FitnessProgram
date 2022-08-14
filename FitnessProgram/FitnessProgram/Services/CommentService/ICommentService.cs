namespace FitnessProgram.Services.CommentService
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Comment;

    public interface ICommentService
    {
        public CommentViewModel GetNewComment(string postId);

        public void Comment(string postId, string message, string userId);

        public List<Comment> GetAll(string postId);

        public void Edit(int commentId, string message);

        public string GetMessage(int commentId);

        public void Delete(int commentId);

        public int GetCommentsCount(string postId);
    }
}
