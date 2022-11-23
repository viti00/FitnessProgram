namespace FitnessProgram.Test.Services
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Services.CommentService;
    using FitnessProgram.Test.Mocks;
    using FitnessProgram.ViewModels.Comment;

    public class CommentsServiceTest
    {
        [Fact]
        public void GetCommentsCountShoudReturnCorrectResultForGivenPost()
        {
            using var data = DatabaseMock.Instance;
            var commentService = new CommentService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var firstPostId = data.Posts.First().Id;
            var lastPostId = data.Posts.Last().Id;
            var userId = data.Users.First().Id;

            data.Comments.Add(new Comment { PostId = firstPostId, Message = "new comment", CreatorId = userId });
            data.SaveChanges();

            Assert.Equal(1, data.Posts.FirstOrDefault(x => x.Id == firstPostId).Comments.Count());
            Assert.Equal(0, data.Posts.FirstOrDefault(x => x.Id == lastPostId).Comments.Count());
        }

        [Fact]
        public void CommentShoudAddNewCommentWhenAllDataIsCorrect()
        {
            const string message = "new message";
            int expectedCount = 1;
            using var data = DatabaseMock.Instance;
            var commentService = new CommentService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;

            commentService.Comment(postId, message, userId);

            Assert.Equal(expectedCount, commentService.GetCommentsCount(postId));
        }

        [Fact]
        public void CommentShoudNotAddNewCommentWhenDataIsIncorrect()
        {
            const string message = "new message";
            int expectedCount = 0;
            using var data = DatabaseMock.Instance;
            var commentService = new CommentService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;

            commentService.Comment("", message, "");

            Assert.Equal(expectedCount, data.Comments.Count());
        }

        [Fact]
        public void DeleteShoudRemoveCorrectCommentWhenGivenIdExists()
        {
            const string message = "new message";
            int expectedCount = 0;
            using var data = DatabaseMock.Instance;
            var commentService = new CommentService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;
            commentService.Comment(postId, message, userId);

            commentService.Delete(1);

            Assert.Equal(expectedCount, commentService.GetCommentsCount(postId));
            Assert.Equal(expectedCount, data.Comments.Count());
        }

        [Fact]
        public void EditShoudChangeCommentDataIfGivenIdExists()
        {
            const string message = "new message";
            using var data = DatabaseMock.Instance;
            var commentService = new CommentService(data);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            data.Comments.AddRange(GetComments(postId));
            data.SaveChanges();

            commentService.Edit(1, message);

            Assert.Equal(message, data.Comments.FirstOrDefault(x => x.Id == 1).Message);
        }

        [Fact]
        public void GetAllShoudReturnAllCommentsForGivenPost()
        {
            using var data = DatabaseMock.Instance;
            var commentService = new CommentService(data);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var firstPostId = data.Posts.First().Id;
            var lastPostId = data.Posts.Last().Id;
            data.Comments.AddRange(GetComments(firstPostId));
            data.SaveChanges();
            int firstPostExpected = 10;
            int lastPostExpected = 0;

            var firstPostComments = commentService.GetAll(firstPostId);
            var lastPostComments = commentService.GetAll(lastPostId);

            Assert.Equal(firstPostExpected, firstPostComments.Count());
            Assert.Equal(lastPostExpected, lastPostComments.Count());
        }

        [Theory]
        [InlineData(1, "Test Comments")]
        [InlineData(3, "Test Comments")]
        [InlineData(0, null)]
        [InlineData(11, null)]
        public void GetMessageShoudReturnCorrectMessageIfCommentIdExists(int commentId, string? message)
        {
            using var data = DatabaseMock.Instance;
            var commentService = new CommentService(data);
            data.Posts.AddRange(GetPosts());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            data.Comments.AddRange(GetComments(postId));
            data.SaveChanges();

            var currMessage = commentService.GetMessage(commentId);

            Assert.Equal(message, currMessage);
        }

        [Fact]
        public void GetNewCommentShoudReturnLastCreatedCommentForGivenPost()
        {
            const string message = "new message";
            int expectedId = 11;
            using var data = DatabaseMock.Instance;
            var commentService = new CommentService(data);
            data.Posts.AddRange(GetPosts());
            data.Users.Add(GetUser());
            data.SaveChanges();
            var postId = data.Posts.First().Id;
            var userId = data.Users.First().Id;
            data.Comments.AddRange(GetComments(postId));
            data.SaveChanges();
            commentService.Comment(postId, message, userId);

            var comment =  commentService.GetNewComment(postId);

            Assert.NotNull(comment);
            Assert.IsType<CommentViewModel>(comment);
            Assert.Equal(message, comment.Message);
            Assert.Equal(expectedId, comment.Id);
        }
    }
}
