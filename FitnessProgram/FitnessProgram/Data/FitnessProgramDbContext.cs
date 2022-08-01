using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessProgram.Data
{
    public class FitnessProgramDbContext : IdentityDbContext
    {
        public FitnessProgramDbContext(DbContextOptions<FitnessProgramDbContext> options)
            : base(options)
        {
        }
    }
}