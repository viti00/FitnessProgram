namespace FitnessProgram.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserLikedPost
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(Post))]
        public string PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
