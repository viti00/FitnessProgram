namespace FitnessProgram.Controllers
{
    using FitnessProgram.Infrastructure;
    using FitnessProgram.Services.CommentService;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
            => this.commentService = commentService;


        public IActionResult Comment(string id, string message)
        {
            commentService.Comment(id, message, User.GetId());

            return Ok();

        }

        public string GetMessage(int id) 
            => commentService.GetMessage(id);

        public IActionResult Edit(int id, string message)
        {
            commentService.Edit(id, message);
            return Ok();
        }

        public IActionResult Delete(int id)
        {
            commentService.Delete(id);

            return Ok();
        }

        public int CommentsCount(string id)
            => commentService.GetCommentsCount(id);

    }
}
