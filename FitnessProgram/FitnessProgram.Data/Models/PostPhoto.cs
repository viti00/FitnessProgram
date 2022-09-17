namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class PostPhoto : Photo
    {
        [ForeignKey(nameof(Post))]
        public string PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
