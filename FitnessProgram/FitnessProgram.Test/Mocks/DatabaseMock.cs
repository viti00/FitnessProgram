namespace FitnessProgram.Test.Mocks
{
    using FitnessProgram.Data;
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseMock
    {
        public static FitnessProgramDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<FitnessProgramDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new FitnessProgramDbContext(dbContextOptions);
            }
        }
    }
}
