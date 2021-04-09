using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Model.Binding;
using AuthService.Model.Entities;

namespace AuthService.Interface.Repository
{
    public interface IUserRepository
    {
        Task<bool> CheckIfUserExistsAsync(string email);
        Task<ApplicationUser> CreateUserAsync(UserRegisterBindingModel model);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<List<string>> GetRolesAsync(ApplicationUser user);
        Task<List<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByNameAsync(string authAppUser);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        List<ApplicationUser> GetUsers(string searchTerm);
        Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> CheckPasswordSignInAsync(ApplicationUser user, string password);
        Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user);
        Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);
        Task<ApplicationUser> AddUserToRoleAsync(ApplicationUser user, string role);
        Task<ApplicationUser> RemoveUserFromRoleAsync(ApplicationUser user, string role);
        Task<ApplicationUser> UpdateUserAsync(ApplicationUser user);
        Task<ApplicationUser> DeleteUserAsync(string id);
        Task<ApplicationUser> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<ApplicationUser> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);
        Task<ApplicationUser> ResetPasswordAsync(ApplicationUser user, string token, string password);
    }
}