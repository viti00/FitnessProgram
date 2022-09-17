namespace FitnessProgram.Controllers.Api
{
    using FitnessProgram.ViewModels.Comment;
    using FitnessProgram.Services.CommentService;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/comments")]

    public class CommentsApiController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentsApiController(ICommentService commentService)
            => this.commentService = commentService;

        [Route("{id}")]
        public CommentViewModel GetComment(string id)
        {
            var comment = commentService.GetNewComment(id);

            return comment;
        }
    }
}
