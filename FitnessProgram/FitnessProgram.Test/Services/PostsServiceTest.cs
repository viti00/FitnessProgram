namespace FitnessProgram.Test.Services
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Services.CommentService;
    using FitnessProgram.Services.LikeService;
    using FitnessProgram.Services.PostServices;
    using FitnessProgram.Test.Mocks;
    using FitnessProgram.ViewModels.Post;
    using Moq;

    public class PostsServiceTest
    {
        [Fact]
        public void CreateShoudAddPostToDatabase()
        {
            int expectedCount = 1;
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);

            postService.Create(GetPost(), GetUser().Id);

            Assert.Equal(expectedCount, data.Posts.Count());
            Assert.Equal("New Title", data.Posts.First().Title);
        }
        [Fact]
        public void GetPostByIdShoudReturnCorrectPost() 
        {
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var postId = data.Posts.First().Id;

            var result =  postService.GetPostById(postId);

            Assert.NotNull(result);
            Assert.IsType<Post>(result);
            Assert.Equal(postId, result.Id);
        }
        [Fact]
        public void GetPostByIdShoudReturnNullIfPostNotExist()
        {
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);

            var result = postService.GetPostById("1");

            Assert.Null(result);
        }
        [Fact]
        public void GetPostDetailsShoudReturnCorrectPostDetails()
        {
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var postId = data.Posts.First().Id;

            var result = postService.GetPostDetails(postId, null);

            Assert.NotNull(result);
            Assert.IsType<PostDetailsModel>(result);
            Assert.Equal(postId, result.Id);
        }
        [Fact]
        public void GetPostDetailsShoudReturnNullIfPostNotExist()
        {
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var postId = data.Posts.First().Id;

            var result = postService.GetPostDetails("1", null);

            Assert.Null(result);
        }
        [Fact]
        public void GetPostDetailsShoudSetPostAlreadyLikedTrueIfCurrentUserAlreadyLikedGivenPost()
        {
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;
            likeSevvice.LikePost(postId, userId);

            var result = postService.GetPostDetails(postId, userId);

            Assert.NotNull(result);
            Assert.IsType<PostDetailsModel>(result);
            Assert.Equal(true, result.IsCurrUserLikedPost);
        }
        [Fact]
        public void GetPostDetailsShoudSetPostAlreadyLikedFalseIfCurrentUserNotLikedGivenPost()
        {
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;

            var result = postService.GetPostDetails(postId, userId);

            Assert.NotNull(result);
            Assert.IsType<PostDetailsModel>(result);
            Assert.Equal(false, result.IsCurrUserLikedPost);
        }
        [Fact]
        public void CreateEditModelShoudReturnEditModelWithCorrectDataIfPostExist()
        {
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var post = postService.GetPostById(postId);

            var result = postService.CreateEditModel(post);

            Assert.NotNull(result);
            Assert.IsType<PostFormModel>(result);
            Assert.Equal(post.Title, result.Title);
            Assert.Equal(post.Text, result.Text);
        }
        [Fact]
        public void EditShoudUpdateGivenPostData()
        {
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var oldPost = postService.GetPostById(postId);
            var text = oldPost.Text;
            var title = oldPost.Title;

            postService.Edit(GetPost(), postId);
            var updatedPost = postService.GetPostById(postId);

            Assert.NotEqual(text, updatedPost.Text);
            Assert.NotEqual(title, updatedPost.Title);
            Assert.Equal("New Title", updatedPost.Title);
            Assert.Equal("New Text", updatedPost.Text);
        }
        [Fact]
        public void DeleteShoudRemovePostLikesForPostAndCommentsForPost()
        {
            int expectedCount = 9;
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var post = postService.GetPostById(postId);

            postService.Delete(post);

            Assert.False(data.Posts.Any(x => x.Id == postId));
            Assert.False(data.Comments.Any(x => x.PostId == postId));
            Assert.False(data.UserLikedPosts.Any(x => x.PostId == postId));
            Assert.Equal(9, data.Posts.Count());
        }
        [Fact]
        public void GetMyShoudReturnOnlyUserPosts()
        {
            int expectedCount = 1;
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var userId = data.Users.First().Id;

            postService.Create(GetPost(), userId);
            var result = postService.GetMy(userId, 1, 6, new AllPostsQueryModel());

            Assert.NotNull(result);
            Assert.IsType<AllPostsQueryModel>(result);
            Assert.Equal(1, result.Posts.Count());
        }
        [Fact]
        public void GetMyShoudSetCorrectMaxPage()
        {
            int expectedCount = 1;
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var userId = data.Users.First().Id;

            postService.Create(GetPost(), userId);
            var result = postService.GetMy(userId, 1, 6, new AllPostsQueryModel());

            Assert.Equal(expectedCount, result.MaxPage);
        }
        [Theory]
        [InlineData(0, 1)]
        [InlineData(2, 1)]
        [InlineData(1, 1)]
        public void GetMyShoudSetCorrectCurrPage(int currPage, int expected)
        {
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var userId = data.Users.First().Id;

            postService.Create(GetPost(), userId);
            var result = postService.GetMy(userId, currPage, 6, new AllPostsQueryModel());

            Assert.Equal(expected, result.CurrentPage);
        }
        [Theory]
        [InlineData("pesho", 0)]
        [InlineData("New", 1)]
        public void GetMyShoudReturnCorrectPostsBySearchTerm(string searchTerm, int expectedCount)
        {
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var userId = data.Users.First().Id;

            postService.Create(GetPost(), userId);
            var result = postService.GetMy(userId, 1, 6, new AllPostsQueryModel { SearchTerm = searchTerm});

            Assert.Equal(expectedCount, result.Posts.Count());
        }

        [Theory]
        [InlineData(Sorting.Default, "New Title 2")]
        [InlineData(Sorting.DateAscending, "New Title")]
        [InlineData(Sorting.LikesAscending, "New Title 2")]
        [InlineData(Sorting.LikesDescending, "New Title")]
        [InlineData(Sorting.CommentsDescending, "New Title")]
        [InlineData(Sorting.CommentsAscending, "New Title 2")]
        public void GetMyShoudReturnCorrectPostsOrderByGivenSorting(Sorting sorting, string expectedTitleFirstPost)
        {
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.SaveChanges();
            var userId = data.Users.First().Id;
            postService.Create(GetPost(), userId);
            postService.Create(new PostFormModel { Title = "New Title 2", Text = "New Text 2" }, userId);
            var postId = data.Posts.First().Id;
            likeSevvice.LikePost(postId, userId);
            commentSevvice.Comment(postId, "New Comment", userId);

            var result = postService.GetMy(userId, 1, 6, new AllPostsQueryModel { Sorting = sorting });

            Assert.Equal(expectedTitleFirstPost, result.Posts.First().Title);
        }

        [Theory]
        [InlineData(1, 6)]
        [InlineData(2, 4)]
        public void GetAllShoudReturnAllPosts(int currPage, int expected)
        {
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();

            var result = postService.GetAll(currPage, 6, new AllPostsQueryModel(), false);

            Assert.NotNull(result);
            Assert.IsType<AllPostsQueryModel>(result);
            Assert.Equal(expected, result.Posts.Count());
        }

        [Fact]
        public void GetAllTheCacheShoudNotAvailableForAdministatorUsers()
        {
            int expectedCache = 0;
            int expectedCount = 5;
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var result = postService.GetAll(2, 6, new AllPostsQueryModel(), true);
            postService.Create(GetPost(), GetUser().Id);
            result = postService.GetAll(2, 6, new AllPostsQueryModel(), true);

            Assert.NotNull(result);
            Assert.IsType<AllPostsQueryModel>(result);
            Assert.Equal(expectedCount, result.Posts.Count());
            Assert.Equal(expectedCache, cache.Count);
        }
        [Fact]
        public void GetAllTheCacheShoudAvailableForNotAdministatorUsers()
        {
            int expectedCache = 1;
            int expectedCount = 4;
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var result = postService.GetAll(2, 6, new AllPostsQueryModel(), false);
            postService.Create(GetPost(), GetUser().Id);
            result = postService.GetAll(2, 6, new AllPostsQueryModel(), false);

            Assert.NotNull(result);
            Assert.IsType<AllPostsQueryModel>(result);
            Assert.Equal(expectedCount, result.Posts.Count());
            Assert.Equal(expectedCache, cache.Count);
        }
        [Fact]
        public void GetAllShoudSetCorrectMaxPage()
        {
            int expectedCount = 2;
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var result = postService.GetAll(1, 6, new AllPostsQueryModel(), false);

            Assert.Equal(expectedCount, result.MaxPage);
        }
        [Theory]
        [InlineData(0, 1)]
        [InlineData(2, 2)]
        [InlineData(1, 1)]
        [InlineData(12, 2)]
        public void GetALLShoudSetCorrectCurrPage(int currPage, int expected)
        {
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var result = postService.GetAll(currPage, 6, new AllPostsQueryModel(), false);

            Assert.Equal(expected, result.CurrentPage);
        }

        [Theory]
        [InlineData("pesho", 0, 1)]
        [InlineData("New", 1, 1)]
        [InlineData("Test", 6, 1)]
        [InlineData("Test", 4, 2)]
        public void GetAllShoudReturnCorrectPostsBySearchTerm(string searchTerm, int expectedCount, int currPage)
        {
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            postService.Create(GetPost(), GetUser().Id);

            var result = postService.GetAll(currPage, 6, new AllPostsQueryModel { SearchTerm = searchTerm }, false);

            Assert.Equal(expectedCount, result.Posts.Count());
        }

        [Theory]
        [InlineData(Sorting.Default, "New Title")]
        [InlineData(Sorting.DateAscending, "Test Posts")]
        [InlineData(Sorting.LikesAscending, "Test Posts")]
        [InlineData(Sorting.LikesDescending, "New Title")]
        [InlineData(Sorting.CommentsDescending, "New Title")]
        [InlineData(Sorting.CommentsAscending, "Test Posts")]
        public void GetAllShoudReturnCorrectPostsOrderByGivenSorting(Sorting sorting, string expectedTitleFirstPost)
        {
            var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var commentSevvice = new CommentService(data);
            var likeSevvice = new LikeService(data);
            var postService = new PostService(data, commentSevvice, likeSevvice, cache);
            data.Users.Add(GetUser());
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var userId = data.Users.First().Id;
            postService.Create(GetPost(), userId);
            var postId = data.Posts.Last().Id;
            likeSevvice.LikePost(postId, userId);
            commentSevvice.Comment(postId, "New Comment", userId);

            var result = postService.GetAll(1, 6, new AllPostsQueryModel { Sorting = sorting }, false);

            Assert.Equal(expectedTitleFirstPost, result.Posts.First().Title);
        }
    }
}
