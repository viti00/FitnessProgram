﻿namespace FitnessProgram.Services.PostServices
{
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Comment;
    using FitnessProgram.Models.Post;
    using FitnessProgram.Services.CommentService;
    using FitnessProgram.Services.LikeService;
    using Microsoft.Extensions.Caching.Memory;
    using static SharedMethods;

    public class PostService : IPostService
    {
        private readonly FitnessProgramDbContext context;
        private readonly ICommentService commentService;
        private readonly ILikeService likeService;
        private readonly IMemoryCache cache;

        public PostService(FitnessProgramDbContext context,
                            ICommentService commentService,
                            ILikeService likeService,
                            IMemoryCache cache)
        {
            this.context = context;
            this.commentService = commentService;
            this.likeService = likeService;
            this.cache = cache;
        }
        public void Create(PostFormModel model, string creatorId)
        {

            var post = new Post
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl == null ? "https://www.salonlfc.com/wp-content/uploads/2018/01/image-not-found-scaled-1150x647.png" : model.ImageUrl,
                Text = model.Text,
                CreatedOn = DateTime.Now,
                Likes = new List<UserLikedPost>(),
                Comments = new List<Comment>(),
                CreatorId = creatorId
            };

            context.Posts.Add(post);
            context.SaveChanges();
        }

        public AllPostsQueryModel GetAll(int currPage, int postPerPage, bool isAdministrator)
        {
            int totalPosts;

            const string postsCache = "PostCache";

            List<Post> postsAll;

            List<PostViewModel> currPagePosts;

            if (isAdministrator)
            {
                totalPosts = context.Posts.Count();

                currPagePosts = context.Posts
                .OrderByDescending(x => x.CreatedOn)
                .Skip((currPage - 1) * postPerPage)
                .Take(postPerPage)
                .Select(x => new PostViewModel
                {
                    PostId = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl == null ? "https://www.salonlfc.com/wp-content/uploads/2018/01/image-not-found-scaled-1150x647.png" : x.ImageUrl,
                    LikesCount = x.Likes.Count(),
                    CommentsCount = x.Comments.Count(),
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),

                })
                .ToList();
            }
            else
            {
                postsAll = cache.Get<List<Post>>(postsCache);
                if(postsAll == null)
                {
                    postsAll = context.Posts
                    .OrderByDescending(x => x.CreatedOn)
                    .Select(x=> new Post
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ImageUrl = x.ImageUrl,
                        Text = x.Text,
                        CreatedOn = x.CreatedOn,
                        Likes = x.Likes,
                        Comments = x.Comments,
                        CreatorId = x.CreatorId
                    })
                    .ToList();

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                    cache.Set(postsCache, postsAll, cacheOptions);
                }

                totalPosts = postsAll.Count();

                currPagePosts =
                    postsAll
                    .Skip((currPage - 1) * postPerPage)
                    .Take(postPerPage)
                    .Select(x => new PostViewModel
                    {
                        PostId = x.Id,
                        Title = x.Title,
                        ImageUrl = x.ImageUrl == null ? "https://www.salonlfc.com/wp-content/uploads/2018/01/image-not-found-scaled-1150x647.png" : x.ImageUrl,
                        LikesCount = x.Likes.Count(),
                        CommentsCount = x.Comments.Count(),
                        CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                    }).ToList();
            }

            var maxPage = CalcMaxPage(totalPosts, postPerPage);

            currPage = GetCurrPage(currPage, maxPage);

            var result = new AllPostsQueryModel
            {
                Posts = currPagePosts,
                CurrentPage = currPage,
                MaxPage = maxPage
            };

            return result;
        }

        public PostDetailsModel GetPostDetails(string postId, string userId)
        {
            var alreadyLiked = context.userLikedPosts.Any(x => x.PostId == postId && x.UserId == userId);

            var post = context.Posts
                .Where(x => x.Id == postId)
                .Select(x => new PostDetailsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    Text = x.Text,
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                    LikesCount = x.Likes.Count(),
                    IsCurrUserLikedPost = alreadyLiked,
                    Comments = x.Comments
                                .OrderByDescending(x => x.CreatedOn)
                                .Select(x=> new CommentViewModel
                                {
                                    Id = x.Id,
                                    Message = x.Message,
                                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                                    UserProfilePictire = x.Creator.ProfilePicture,
                                    UserUsername = x.Creator.UserName,
                                    UserId = x.CreatorId
                                })
                                .ToList(),
                    Creator = new UserViewModel
                    {
                        Id = x.CreatorId,
                        ProfilePicture = x.Creator.ProfilePicture,
                        Username = x.Creator.UserName,
                    }
                }).FirstOrDefault();

            return post;
        }

        public PostFormModel CreateEditModel(Post post)
        {
            var model = new PostFormModel
            {
                Title = post.Title,
                ImageUrl = post.ImageUrl,
                Text = post.Text
            };

            return model;
        }

        public void Edit(PostFormModel model, string postId)
        {
            var post = GetPostById(postId);

            post.Title = model.Title;
            post.ImageUrl = model.ImageUrl;
            post.Text = model.Text;

            context.SaveChanges();
        }

        public void Delete(Post post)
        {
            var comments = commentService.GetAll(post.Id);

            context.Comments.RemoveRange(comments);

            var likes = likeService.GetAllLikesForPost(post.Id);

            context.userLikedPosts.RemoveRange(likes);

            context.Posts.Remove(post);
            context.SaveChanges();
        }

        public Post GetPostById(string postId)
            => context.Posts.FirstOrDefault(x => x.Id == postId);
    }
}
