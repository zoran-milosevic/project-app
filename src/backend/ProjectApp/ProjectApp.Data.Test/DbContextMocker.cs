using Microsoft.EntityFrameworkCore;
using ProjectApp.Data.Context;

namespace ProjectApp.Data.Test
{
    public static class DbContextMocker
    {
        public static AppDbContext GetAppDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new AppDbContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
