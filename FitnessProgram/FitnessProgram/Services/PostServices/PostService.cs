namespace FitnessProgram.Services.PostServices
{
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Post;


    public class PostService : IPostService
    {
        private readonly FitnessProgramDbContext context;

        public PostService(FitnessProgramDbContext context)
            => this.context = context;

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

        public PostDetailsModel GetPostById(string postId)
        {
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
                    Comments = x.Comments.OrderByDescending(x => x.CreatedOn).ToList(),
                    Creator = new UserViewModel
                    {
                        Id = x.CreatorId
                    }
                }).FirstOrDefault();

            return post;
        }

        public PostFormModel CreateEditModel(string postId)
        {
            var post = GetPostById(postId);

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
            var post = context.Posts.FirstOrDefault(x => x.Id == postId);

            post.Title = model.Title;
            post.ImageUrl = model.ImageUrl;
            post.Text = model.Text;

            context.Posts.Update(post);
            context.SaveChanges();
        }

        public bool IsCreator(string postId, string userId)
            => context.Posts.Any(x => x.Id == postId && x.CreatorId == userId);
    }
}
