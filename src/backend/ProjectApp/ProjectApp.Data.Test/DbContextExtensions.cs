using System;
using ProjectApp.Data.Context;
using ProjectApp.Model.Entities.User;

namespace ProjectApp.Data.Test
{
    public static class DbContextExtensions
    {
        public static void Seed(this AppDbContext dbContext)
        {
            // Add entities for DbContext instance

            dbContext.Client.AddAsync(new Client
            {
                UserProfileId = 1,
                FirstName = "Jon",
                LastName = "Doe",
                UserType = 2,
                Username = "john.doe@test.test",
                Email = "john.doe@test.test",
                PhoneNumber = "+38166123456"
            });

            dbContext.InternalUser.AddAsync(new InternalUser
            {
                UserProfileId = 1,
                FirstName = "Jane",
                LastName = "Rowan",
                UserType = 1,
                Username = "jane.rowan@test.test",
                Email = "jane.rowan@test.test",
                PhoneNumber = "+38166654321"
            });

            dbContext.SaveChanges();
        }
    }
}
