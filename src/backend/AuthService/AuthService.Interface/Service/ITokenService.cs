using System.Threading.Tasks;
using AuthService.Model.Entities;

namespace AuthService.Interface.Service
{
    public interface ITokenService
    {
        Task<string> GetToken(ApplicationUser user);
    }
}