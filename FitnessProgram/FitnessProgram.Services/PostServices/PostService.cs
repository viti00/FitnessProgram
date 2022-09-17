namespace FitnessProgram.Services.PostServices
{
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.ViewModels.Comment;
    using FitnessProgram.ViewModels.Post;
    using FitnessProgram.Services.CommentService;
    using FitnessProgram.Services.LikeService;
    using Microsoft.Extensions.Caching.Memory;
    using static SharedMethods;
    using Microsoft.AspNetCore.Http;

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
            var photos = CreatePhotos(model.Files);

            var post = new Post
            {
                Title = model.Title,
                Photos = photos,
                Text = model.Text,
                CreatedOn = DateTime.Now,
                Likes = new List<UserLikedPost>(),
                Comments = new List<Comment>(),
                CreatorId = creatorId,
            };

            context.Posts.Add(post);
            context.SaveChanges();
        }

        public AllPostsQueryModel GetMy(string userId, int currPage, int postPerPage)
        {
            int totalPosts = context.Posts.Where(x => x.CreatorId == userId).Count();

            int maxPage = CalcMaxPage(totalPosts, currPage);
            currPage = GetCurrPage(currPage, maxPage);

            var myPosts = context.Posts
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.CreatorId == userId)
                .Skip((currPage - 1) * postPerPage)
                .Take(postPerPage)
                .Select(x => new PostViewModel
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Photos = x.Photos.Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
                    LikesCount = x.Likes.Count(),
                    CommentsCount = x.Comments.Count(),
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),

                })
                .ToList();
            var result = new AllPostsQueryModel
            {
                Posts = myPosts,
                MaxPage = maxPage,
                CurrentPage = currPage
            };

            return result;
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
                    Photos = x.Photos.Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
                    LikesCount = x.Likes.Count(),
                    CommentsCount = x.Comments.Count(),
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),

                })
                .ToList();
            }
            else
            {
                postsAll = cache.Get<List<Post>>(postsCache);
                if (postsAll == null)
                {
                    postsAll = context.Posts
                    .OrderByDescending(x => x.CreatedOn)
                    .Select(x => new Post
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Photos = x.Photos,
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
                        Photos = x.Photos.Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
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
            var alreadyLiked = context.UserLikedPosts.Any(x => x.PostId == postId && x.UserId == userId);

            var post = context.Posts
                .Where(x => x.Id == postId)
                .Select(x => new PostDetailsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Photos = x.Photos.Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
                    Text = x.Text,
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                    LikesCount = x.Likes.Count(),
                    IsCurrUserLikedPost = alreadyLiked,
                    Comments = x.Comments
                                .OrderByDescending(x => x.CreatedOn)
                                .Select(x => new CommentViewModel
                                {
                                    Id = x.Id,
                                    Message = x.Message,
                                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),
                                    UserProfilePictire = x.Creator.ProfilePicture != null ? Convert.ToBase64String(x.Creator.ProfilePicture.Bytes) : AnonymousImageConstant.AnonymousImage,
                                    UserUsername = x.Creator.UserName,
                                    UserId = x.CreatorId
                                })
                                .ToList(),
                    Creator = new UserViewModel
                    {
                        Id = x.CreatorId,
                        ProfilePicture = x.Creator.ProfilePicture != null ? Convert.ToBase64String(x.Creator.ProfilePicture.Bytes) : AnonymousImageConstant.AnonymousImage,
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
                Text = post.Text
            };

            return model;
        }

        public void Edit(PostFormModel model, string postId)
        {
            var post = GetPostById(postId);
            post.Photos = GetPhotos(postId); // this method get old photos

            var photos = CreatePhotos(model.Files);

            post.Title = model.Title;
            post.Text = model.Text;
            post.Photos = photos;

            context.SaveChanges();
        }

        public void Delete(Post post)
        {
            var comments = commentService.GetAll(post.Id);

            context.Comments.RemoveRange(comments);

            var likes = likeService.GetAllLikesForPost(post.Id);

            context.UserLikedPosts.RemoveRange(likes);

            context.Posts.Remove(post);
            context.SaveChanges();
        }

        public Post GetPostById(string postId)
            => context.Posts
            .FirstOrDefault(x => x.Id == postId);
           

        private List<PostPhoto> CreatePhotos(IFormFileCollection files)
        {
            List<PostPhoto> photos = new List<PostPhoto>();
            Task.Run(async () =>
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            if (memoryStream.Length < 2097152)
                            {
                                var newphoto = new PostPhoto()
                                {
                                    Bytes = memoryStream.ToArray(),
                                    Description = file.FileName,
                                    FileExtension = Path.GetExtension(file.FileName),
                                    Size = file.Length,
                                };
                                photos.Add(newphoto);
                            }
                        }
                    }
                }
            }).GetAwaiter()
               .GetResult();

            return photos;
        }

        private List<PostPhoto> GetPhotos(string postId)
            => context.PostPhotos.Where(x => x.PostId == postId).ToList();
    }
}
