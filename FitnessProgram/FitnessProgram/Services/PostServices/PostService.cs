namespace FitnessProgram.Services.PostServices
{
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Comment;
    using FitnessProgram.Models.Post;
    using FitnessProgram.Services.CommentService;
    using FitnessProgram.Services.LikeService;

    public class PostService : IPostService
    {
        private readonly FitnessProgramDbContext context;
        private readonly ICommentService commentService;
        private readonly ILikeService likeService;

        public PostService(FitnessProgramDbContext context, ICommentService commentService, ILikeService likeService)
        {
            this.context = context;
            this.commentService = commentService;
            this.likeService = likeService;
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

        public AllPostsQueryViewModel GetAll(int currPage, int postPerPage)
        {
            var totalPosts = context.Posts.Count();

            var maxPage = (int)Math.Ceiling((double)totalPosts / postPerPage);

            if (currPage > maxPage)
            {
                if(maxPage == 0)
                {
                    maxPage = 1;
                }
                currPage = maxPage;
            }

            var posts = context.Posts
                .OrderByDescending(x=> x.CreatedOn)
                .Skip((currPage-1)*postPerPage)
                .Take(postPerPage)
                .Select(x => new PostViewModel
                {
                    PostId = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl == null ? "https://www.salonlfc.com/wp-content/uploads/2018/01/image-not-found-scaled-1150x647.png": x.ImageUrl,
                    LikesCount = x.Likes.Count(),
                    CommentsCount = x.Comments.Count(),
                    CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy HH:mm"),

                })
                .ToList();

            var result = new AllPostsQueryViewModel
            {
                Posts = posts,
                CurrentPage = currPage,
                MaxPage = maxPage,
                PostCount = totalPosts
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
                                    UserUsername = x.Creator.UserName
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
