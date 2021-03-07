using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectApp.Model.Entities.User;

namespace ProjectApp.Interface.Service
{
    public interface IUserProfileService
    {
        Task<IEnumerable<UserProfile>> OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId);
        Task<UserProfile> GetByIdAsync(int id);
        Task<IEnumerable<UserProfile>> GetAllAsync();
        Task<UserProfile> CreateAsync(UserProfile model);
    }
}