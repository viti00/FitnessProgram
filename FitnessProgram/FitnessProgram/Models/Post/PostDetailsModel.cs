
namespace FitnessProgram.Models.Post
{
    using FitnessProgram.Data.Models;

    public class PostDetailsModel
    {
        public string Id { get; init; }

        public string Title { get; init; }

        public string? ImageUrl { get; init; }

        public string Text { get; init; }

        public string CreatedOn { get; init; }

        public int LikesCount { get; set; }

        public ICollection<Comment> Comments { get; init; }

        public UserViewModel Creator { get; init; }
    }


    public class UserViewModel
    {
        public string Id { get; init; }

        public string? ProfilePicture { get; init; }

        public string? Username { get; init; }
    }
}
