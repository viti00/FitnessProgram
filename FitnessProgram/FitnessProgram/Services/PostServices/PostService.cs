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

        public List<PostViewModel> GetAll()
        {
            var posts = context.Posts
                .OrderBy(x=> x.CreatedOn)
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

            return posts;
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
                    Comments = x.Comments,
                    CreatorId = x.CreatorId
                }).FirstOrDefault();

            return post;
        }
    }
}
