namespace FitnessProgram.Models.Post
{
    public class PostEditModel
    {
        public string Title { get; init; }

        public string? ImageUrl { get; init; }

        public string Text { get; init; }

        public PostFormModel FormModel { get; init; }
    }
}
