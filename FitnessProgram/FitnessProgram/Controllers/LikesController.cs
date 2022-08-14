namespace FitnessProgram.Controllers
{
    using FitnessProgram.Infrastructure;
    using FitnessProgram.Services.LikeService;
    using Microsoft.AspNetCore.Mvc;


    public class LikesController : Controller
    {
        private readonly ILikeService likeService;

        public LikesController(ILikeService likeService) 
            => this.likeService = likeService;

        public IActionResult LikePost(string id)
        {
            likeService.LikePost(id, User.GetId());

            return Ok();
        }

        public IActionResult UnlikePost(string id)
        {
            likeService.UnlikePost(id, User.GetId());

            return Ok();
        }
    }
}
