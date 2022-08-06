﻿namespace FitnessProgram.Controllers
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

        public IActionResult All([FromQuery] AllPostsQueryViewModel query)
        {
            var AllPostModel = postService.GetAll(query.CurrentPage, AllPostsQueryViewModel.PostPerPage);

            return View(AllPostModel);
        }

        [Authorize]
        public IActionResult Create()
        {
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

        [Authorize]
        public IActionResult Edit(string id)
        {
            var model = postService.CreateEditModel(id);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(PostFormModel edit, string id)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }

            postService.Edit(edit, id);

            return Redirect($"/Posts/Details/{id}");
        }

        public IActionResult Details(string id)
        {

            var post = postService.GetPostById(id);

            return View(post);
        }
    }
}
