using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectApp.Data;
using ProjectApp.Model.Entities.User;
using ProjectApp.Interface.Service;

namespace ProjectApp.Service
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ILogger<UserProfileService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(ILogger<UserProfileService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return _unitOfWork.UserProfileRepository().GetAll();
            });
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            return await _unitOfWork.UserProfileRepository().GetById(id);
        }

        public async Task<UserProfile> CreateAsync(UserProfile model)
        {
            await _unitOfWork.UserProfileRepository().Create(model);
            await _unitOfWork.SaveAsync();

            return model;
        }

        public async Task<IEnumerable<UserProfile>> OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId)
        {
            return await _unitOfWork.UserProfileRepository().GetAllWithSomethingElseAsync();
        }
    }
}
