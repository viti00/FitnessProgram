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
    using Microsoft.EntityFrameworkCore;

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

        public AllPostsQueryModel GetMy(string userId, int currPage, int postPerPage, AllPostsQueryModel query)
        {
            var postsAll = context.Posts.Where(x => x.CreatorId == userId).ToList();

            int totalPosts = postsAll.Count();

            int maxPage = CalcMaxPage(totalPosts, postPerPage);
            currPage = GetCurrPage(currPage, ref maxPage);

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                postsAll = Search(postsAll, query.SearchTerm);
            }

            postsAll = Sort(postsAll, query.Sorting);

            var myPosts = CreateViewModel(postsAll, currPage, postPerPage);

            var result = CreateModel(myPosts, currPage, maxPage, query.SearchTerm, query.Sorting);

            return result;
        }

        public AllPostsQueryModel GetAll(int currPage, int postPerPage, AllPostsQueryModel query, bool isAdministrator)
        {
            int totalPosts;

            const string postsCache = "PostCache";

            List<Post> postsAll;

            List<PostViewModel> currPagePosts;


            if (isAdministrator)
            {
                postsAll = GetPosts();
            }
            else
            {
                postsAll = cache.Get<List<Post>>(postsCache);
                if (postsAll == null)
                {
                    postsAll = GetPosts();

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                    cache.Set(postsCache, postsAll, cacheOptions);
                }
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                postsAll = Search(postsAll, query.SearchTerm);
            }

            postsAll = Sort(postsAll, query.Sorting);

            totalPosts = postsAll.Count();

            var maxPage = CalcMaxPage(totalPosts, postPerPage);

            currPage = GetCurrPage(currPage, ref maxPage);

            currPagePosts = CreateViewModel(postsAll, currPage, postPerPage);

            var result = CreateModel(currPagePosts, currPage, maxPage, query.SearchTerm, query.Sorting);

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
            .Include(p=> p.Photos)
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

        private List<Post> Search(List<Post> postsAll,string searchTerm)
            => postsAll
                    .Where(p => p.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

        private List<Post> Sort(List<Post> postsAll, Sorting sorting)
            => postsAll = sorting switch
            {
                Sorting.Default => postsAll.OrderByDescending(x => x.CreatedOn).ToList(),
                Sorting.LikesAscending => postsAll.OrderBy(x => x.Likes.Count()).ThenByDescending(x=> x.CreatedOn).ToList(),
                Sorting.LikesDescending => postsAll.OrderByDescending(x => x.Likes.Count()).ThenByDescending(x => x.CreatedOn).ToList(),
                Sorting.CommentsAscending => postsAll.OrderBy(x => x.Comments.Count()).ThenByDescending(x => x.CreatedOn).ToList(),
                Sorting.CommentsDescending => postsAll.OrderByDescending(x => x.Comments.Count()).ThenByDescending(x => x.CreatedOn).ToList(),
                Sorting.DateAscending => postsAll.OrderBy(x => x.CreatedOn).ToList(),
                _ => postsAll.OrderByDescending(x => x.CreatedOn).ToList()
            };

        private List<PostViewModel> CreateViewModel(List<Post> postsAll, int currPage, int postPerPage)
        {
            var posts = postsAll
            .Skip((currPage - 1) * postPerPage)
            .Take(postPerPage).ToList()
            .Select(x => new PostViewModel
            {
                PostId = x.Id,
                Title = x.Title,
                Photos = x.Photos.Select(x => Convert.ToBase64String(x.Bytes)).ToList(),
                LikesCount = x.Likes.Count(),
                CommentsCount = x.Comments.Count(),
                CreatedOn = x.CreatedOn,

            })
           .ToList();

            return posts;
        }

        private AllPostsQueryModel CreateModel
            (List<PostViewModel> posts, int currPage,int maxPage, string serchTerm, Sorting sorting)
            => new AllPostsQueryModel
            {
                Posts = posts,
                CurrentPage = currPage,
                MaxPage = maxPage,
                SearchTerm = serchTerm,
                Sorting = sorting
            };

        private List<Post> GetPosts()
            => context.Posts
               .Include(l => l.Likes)
               .Include(c => c.Comments)
               .Include(p => p.Photos)
               .ToList();
    }
}
