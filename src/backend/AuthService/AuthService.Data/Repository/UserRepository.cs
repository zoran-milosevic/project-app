using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AuthService.Interface.Repository;
using AuthService.Model.Entities;
using AuthService.Model.Binding;

namespace AuthService.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CheckIfUserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user == null ? false : true;
        }

        public async Task<ApplicationUser> CreateUserAsync(UserRegisterBindingModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Created = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                user.IdentityErrors = result.Errors.Select(q => q.Description).ToList();
            }
            else
            {
                result = await _userManager.AddToRoleAsync(user, "User");
            }

            if (!result.Succeeded)
            {
                user.IdentityErrors = result.Errors.Select(q => q.Description).ToList();
            }

            return user;
        }

        public async Task<List<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }

        public async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            return claims.ToList();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return emailToken;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var emailToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return emailToken;
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string authAppUser)
        {
            var user = await _userManager.FindByNameAsync(authAppUser);

            if (user == null)
            {
                user.IdentityErrors.Add("User not found!");
            }

            return user;
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public List<ApplicationUser> GetUsers(string searchTerm)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = _userManager.Users.Where(q => q.FirstName.Contains(searchTerm) || q.LastName.Contains(searchTerm) || q.Email.Contains(searchTerm) || q.UserName.Contains(searchTerm)).ToList();
            }
            else
            {
                users = _userManager.Users.ToList();
            }

            return users;
        }

        public async Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);

            return users.ToList();
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<bool> CheckPasswordSignInAsync(ApplicationUser user, string password)
        {
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            return signInResult.Succeeded;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            return claims.ToList();
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            var isInRole = await _userManager.IsInRoleAsync(user, roleName);

            return isInRole;
        }

        public async Task<ApplicationUser> AddUserToRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                user.IdentityErrors = result.Errors.Select(q => q.Description).ToList();
            }

            return user;
        }

        public async Task<ApplicationUser> RemoveUserFromRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, role);

            if (!result.Succeeded)
            {
                user.IdentityErrors = result.Errors.Select(q => q.Description).ToList();
            }

            return user;
        }

        public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                user.IdentityErrors = result.Errors.Select(q => q.Description).ToList();
            }

            return user;
        }

        public async Task<ApplicationUser> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(q => q.Description).ToList();

                user.IdentityErrors.AddRange(errors);
            }

            return user;
        }

        public async Task<ApplicationUser> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(q => q.Description).ToList();

                user.IdentityErrors.AddRange(errors);
            }

            return user;
        }

        public async Task<ApplicationUser> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(q => q.Description).ToList();

                user.IdentityErrors.AddRange(errors);
            }

            return user;
        }

        public async Task<ApplicationUser> ResetPasswordAsync(ApplicationUser user, string token, string password)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(q => q.Description).ToList();

                user.IdentityErrors.AddRange(errors);
            }

            return user;
        }
    }
}