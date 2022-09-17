namespace FitnessProgram.ViewModels.Post
{
    using FitnessProgram.ViewModels.Comment;

    public class PostDetailsModel
    {
        public string Id { get; init; }

        public string Title { get; init; }

        public List<string> Photos { get; init; }

        public string Text { get; init; }

        public string CreatedOn { get; init; }

        public int LikesCount { get; set; }

        public bool IsCurrUserLikedPost { get; init; }

        public ICollection<CommentViewModel> Comments { get; init; }

        public UserViewModel Creator { get; init; }
    }


    public class UserViewModel
    {
        public string Id { get; init; }

        public string? ProfilePicture { get; init; }

        public string Username { get; init; }
    }
}
