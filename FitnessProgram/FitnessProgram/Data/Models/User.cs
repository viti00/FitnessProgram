namespace FitnessProgram.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Url]
        public string? ProfilePicture { get; set; }
    }
}
