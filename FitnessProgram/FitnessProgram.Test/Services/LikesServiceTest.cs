namespace FitnessProgram.Test.Services
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Services.LikeService;
    using FitnessProgram.Test.Mocks;

    public class LikesServiceTest
    {
        [Fact]
        public void GetAllLikesForPostShoudReturnLikesForGivenPost()
        {
            int expected = 1;
            using var data = DatabaseMock.Instance;
            var likeService = new LikeService(data);
            var posts = GetPosts();
            var postId = posts.First().Id;
            var user = GetUser();
            data.Posts.AddRange(posts);
            data.Users.Add(user);
            data.UserLikedPosts.Add(new UserLikedPost { PostId = postId, UserId = user.Id});
            data.SaveChanges();

            var result = likeService.GetAllLikesForPost(postId);

            Assert.NotNull(result);
            Assert.IsType<List<UserLikedPost>>(result);
            Assert.Equal(expected, result.Count);
        }
        [Fact]
        public void GetLikesCountShoudReturnCorectLikesCountForGivenPost()
        {
            int expected = 1;
            using var data = DatabaseMock.Instance;
            var likeService = new LikeService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;
            data.UserLikedPosts.Add(new UserLikedPost { PostId = postId, UserId = userId });
            data.SaveChanges();

            var result = likeService.GetLikesCount(postId);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, int.Parse(result));
        }
        [Fact]
        public void LikePostShoudLikeGivenPostIfExists()
        {
            int expected = 1;
            using var data = DatabaseMock.Instance;
            var likeService = new LikeService(data); 
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;
            var oldLikesCount = data.Posts.First().Likes.Count();

            likeService.LikePost(postId, userId);

            Assert.NotEqual(oldLikesCount, data.Posts.First().Likes.Count());
            Assert.Equal(expected, data.Posts.First().Likes.Count());
            Assert.Equal(expected, data.UserLikedPosts.Count());
        }
        [Fact]
        public void LikePostShoudNotLikeGivenPostIfNotExists()
        {
            int expected = 0;
            using var data = DatabaseMock.Instance;
            var likeService = new LikeService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var userId = data.Users.First().Id;

            likeService.LikePost("1", userId);

            Assert.Equal(expected, data.UserLikedPosts.Count());
        }
        [Fact]
        public void LikePostShoudNotLikeGivenPostIfUserNotExists()
        {
            int expected = 0;
            using var data = DatabaseMock.Instance;
            var likeService = new LikeService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;

            likeService.LikePost(postId, null);

            Assert.Equal(expected, data.UserLikedPosts.Count());
        }

        [Fact]
        public void UnlikePostShoudUnlikeGivenPostIfLikeExists()
        {
            int expected = 0;
            using var data = DatabaseMock.Instance;
            var likeService = new LikeService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;
            likeService.LikePost(postId, userId);
            var oldLikesCount = data.Posts.First().Likes.Count();

            likeService.UnlikePost(postId, userId);

            Assert.NotEqual(oldLikesCount, data.Posts.First().Likes.Count());
            Assert.Equal(expected, data.Posts.First().Likes.Count());
            Assert.Equal(expected, data.UserLikedPosts.Count());
        }
        [Fact]
        public void UnlikePostShoudUnlikeGivenPostIfLikeNotExists()
        {
            int expected = 1;
            using var data = DatabaseMock.Instance;
            var likeService = new LikeService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;
            likeService.LikePost(postId, userId);

            likeService.UnlikePost("1", userId);

            Assert.Equal(expected, data.UserLikedPosts.Count());
        }
    }
}
