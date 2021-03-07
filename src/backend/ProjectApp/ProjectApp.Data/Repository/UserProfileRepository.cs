using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ProjectApp.Data.Context;
using ProjectApp.Interface.Repository;
using ProjectApp.Model.Entities.User;

namespace ProjectApp.Data.Repository
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        private readonly ILogger<UserProfileRepository> _logger;
        private readonly AppDbContext _context;

        public UserProfileRepository(ILogger<UserProfileRepository> logger, AppDbContext context) : base(logger, context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<UserProfile>> GetAllWithSomethingElseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
