﻿namespace FitnessProgram.Controllers
{
    using FitnessProgram.Infrastructure;
    using FitnessProgram.ViewModels.Post;
    using FitnessProgram.Services.PostServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult All([FromQuery] AllPostsQueryModel query)
        {
            var isAdministator = User.IsAdministrator();

            var currPagePosts = postService.GetAll(query.CurrentPage, AllPostsQueryModel.PostPerPage,query, isAdministator);

            return View(currPagePosts);
        }

        [Authorize]
        public IActionResult MyPosts([FromQuery] AllPostsQueryModel query)
        {
            var myPosts = postService.GetMy(User.GetId(), query.CurrentPage, AllPostsQueryModel.PostPerPage, query);

            return View(myPosts);
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
            var userId = User.GetId();
            if(userId== null)
            {
                return Unauthorized();
            }

            postService.Create(post, userId);
            return RedirectToAction("MyPosts", "Posts");
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var userId = User.GetId();
            var post = postService.GetPostById(id);

            if(post == null)
            {
                return BadRequest();
            }

            if(!User.IsAdministrator() && post.CreatorId != userId)
            {
                return Unauthorized();
            }

            var model = postService.CreateEditModel(post);

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
            var post = postService.GetPostDetails(id, User.GetId());

            if(post == null)
            {
                return BadRequest();
            }

            return View(post);
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            var userId = User.GetId();
            var post = postService.GetPostById(id);

            if(post == null)
            {
                BadRequest();
            }

            if(!User.IsAdministrator() && post.CreatorId != userId)
            {
                return Unauthorized();
            }

            postService.Delete(post);

            return RedirectToAction("All", "Posts");
        }
    }
}
