namespace FitnessProgram.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {
        [ForeignKey(nameof(ProfilePicture))]
        public int? ProfilePictureId { get; set; }

        public virtual ProfilePhoto? ProfilePicture { get; set; }

        [FromForm]
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
