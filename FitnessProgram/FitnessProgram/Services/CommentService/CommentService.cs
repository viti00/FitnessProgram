namespace FitnessProgram.Services.CommentService
{
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Comment;
    using System.Collections.Generic;

    public class CommentService : ICommentService
    {
        private readonly FitnessProgramDbContext context;

        public CommentService(FitnessProgramDbContext context) 
            => this.context = context;

        public void Comment(string postId, string message, string userId)
        {
            var comment = new Comment
            {
                Message = message,
                CreatedOn = DateTime.Now,
                PostId = postId,
                CreatorId = userId
            };

            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public void Delete(int commentId)
        {
            var comment = GetCommentById(commentId);

            context.Comments.Remove(comment);
            context.SaveChanges();
        }

        public void Edit(int commentId, string message)
        {
            var comment = GetCommentById(commentId);

            comment.Message = message;

            context.SaveChanges();
        }

        public List<Comment> GetAll(string postId)
        {
            var comments = context
                .Comments.Where(x => x.PostId == postId)
                .ToList();

            return comments;
        }

        public int GetCommentsCount(string postId)
            => context.Comments.Where(x => x.PostId == postId).Count();

        public string GetMessage(int commentId)
        {
            var message = context.Comments
                .Where(x => x.Id == commentId)
                .Select(x=> x.Message)
                .FirstOrDefault();

            return message;
        }

        public CommentViewModel GetNewComment(string postId)
        {
            var comment = context.Comments
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.PostId == postId)
                .Select(x => new CommentViewModel
                {
                    Id= x.Id,
                    Message = x.Message,
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                    UserProfilePictire = x.Creator.ProfilePicture,
                    UserUsername = x.Creator.UserName,
                    UserId = x.CreatorId
                }).First();

            return comment;
        }

        private Comment GetCommentById(int id)
            => context.Comments.FirstOrDefault(c => c.Id == id);
    }
}
