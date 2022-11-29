namespace FitnessProgram.Controllers
{
    using FitnessProgram.Infrastructure;
    using FitnessProgram.Services.LikeService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    public class LikesController : Controller
    {
        private readonly ILikeService likeService;

        public LikesController(ILikeService likeService) 
            => this.likeService = likeService;

        [Authorize]
        public IActionResult LikePost(string id)
        {
            likeService.LikePost(id, User.GetId());

            return Ok();
        }

        [Authorize]
        public IActionResult UnlikePost(string id)
        {
            likeService.UnlikePost(id, User.GetId());

            return Ok();
        }
    }
}
