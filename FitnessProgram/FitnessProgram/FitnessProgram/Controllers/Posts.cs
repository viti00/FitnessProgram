namespace FitnessProgram.Controllers
{
    using FitnessProgram.Models.Post;
    using Microsoft.AspNetCore.Mvc;

    public class Posts : Controller
    {
        public IActionResult All()
        {
            ViewData["Title"] = "Posts";
            return View();
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create Post";
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostFormModel post)
        {
            if (!this.ModelState.IsValid)
            {
                return View(post);
            }
            return RedirectToAction("All", "Posts");
        }

        public IActionResult Edit()
        {
            ViewData["Title"] = "Edit Post";
            return View();
        }

        public IActionResult Details(int id)
        {
            ViewData["Title"] = "Post Details";
            return View();
        }
    }
}
