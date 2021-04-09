using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Model.Binding;
using AuthService.Model.DTO;
using AuthService.Model.Entities;

namespace AuthService.Interface.Service
{
    public interface IUserService
    {
        Task<UserDTO> GetUserReturnModel(ApplicationUser user, Uri locationHeader);
        Task<ApplicationUser> RegisterAccountAsync(UserRegisterBindingModel model);
        Task<ApplicationUser> GetUserByNameAsync(string authAppUser);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        List<ApplicationUser> GetUsers(string searchTerm);
        Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> SignInUserAsync(ApplicationUser user, string password);
        Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user);
        Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);
        Task<List<ApplicationUser>> ChangeUserRolesAsync(string authenticatedAppUser, UserRolesBindingModel model);
        Task<ApplicationUser> UpdateUser(string authenticatedAppUser, UserUpdateBindingModel model);
        Task<ApplicationUser> DeleteUser(string authenticatedAppUser, string id);
        Task<ApplicationUser> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<ApplicationUser> ResendEmailConfirmation(ApplicationUser user);
        Task<ApplicationUser> RequestPasswordResetEmail(ApplicationUser user);
        Task<ApplicationUser> ChangePassword(string authenticatedAppUser, UserPasswordChangeBindingModel model);
        Task<ApplicationUser> ResetPasswordAsync(ApplicationUser user, string token, string password);
    }
}