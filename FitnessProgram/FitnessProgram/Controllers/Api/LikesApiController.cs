namespace FitnessProgram.Controllers.Api
{
    using FitnessProgram.Services.LikeService;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/likes")]
    public class LikesApiController : ControllerBase
    {
        private readonly ILikeService likeService;

        public LikesApiController(ILikeService likeService)
            => this.likeService = likeService;

        [HttpGet]
        [Route("{id}")]
        public string LikesCount(string id)
            => likeService.GetLikesCount(id);
    }
}
