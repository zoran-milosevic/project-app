
using System.Linq;
using System.Threading.Tasks;
using ProjectApp.Data.Repository;
using Xunit;

namespace ProjectApp.Data.Test.Tests
{
    public class UserProfileRepositoryUnitTest
    {
        [Fact]
        public void TestGetUserProfilesAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestGetUserProfilesAsync));
            var repository = new UserProfileRepository(null, dbContext);

            // Act
            var response = repository.GetAll();
            var value = response.ToList();

            dbContext.Dispose();

            // Assert
            Assert.False(value.Count == 0);
        }

        [Fact]
        public async Task TestGetUserProfileByIdAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestGetUserProfileByIdAsync));
            var repository = new UserProfileRepository(null, dbContext);

            // Act
            var response = await repository.GetById(1);
            var value = response;

            dbContext.Dispose();

            // Assert
            Assert.False(value == null);
        }
    }
}
