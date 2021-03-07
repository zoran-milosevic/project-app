using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectApp.Model.Entities.User;

namespace ProjectApp.Interface.Repository
{
    public interface IUserProfileRepository : IGenericRepository<UserProfile>
    {
        Task<IEnumerable<UserProfile>> GetAllWithSomethingElseAsync();
    }
}