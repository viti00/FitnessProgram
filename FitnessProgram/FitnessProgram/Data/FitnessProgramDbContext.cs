
namespace FitnessProgram.Data
{
    using FitnessProgram.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;


    public class FitnessProgramDbContext : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Customer> Customers {get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<BestResult> BestResults { get; set; }

        public DbSet<UserLikedPost> userLikedPosts { get; set; }

        public FitnessProgramDbContext(DbContextOptions<FitnessProgramDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserLikedPost>()
                .HasKey(k => new { k.UserId, k.PostId });

            builder.Entity<UserLikedPost>()
                .HasOne(x => x.Post)
                .WithMany(x => x.Likes)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(x => x.Post)
                .WithMany(x=> x.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}