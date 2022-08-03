namespace FitnessProgram.Controllers
{
    using FitnessProgram.Models.Post;
    using FitnessProgram.Services.PostServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class Posts : Controller
    {
        private readonly IPostService postService;

        public Posts(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult All()
        {
            ViewData["Title"] = "Posts";

            var posts = postService.GetAll();

            return View(posts);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create Post";
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PostFormModel post)
        {
            if (!this.ModelState.IsValid)
            {
                return View(post);
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            postService.Create(post, userId);
            return RedirectToAction("All", "Posts");
        }

        public IActionResult Edit()
        {
            ViewData["Title"] = "Edit Post";
            return View();
        }

        public IActionResult Details(string id)
        {
            ViewData["Title"] = "Post Details";
            return View();
        }
    }
}
