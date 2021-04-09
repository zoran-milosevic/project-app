using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Interface.Repository;
using AuthService.Interface.Service;
using AuthService.Model.Entities;
using AuthService.Model.Binding;
using AuthService.Model.DTO;

namespace AuthService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserDTO> GetUserReturnModel(ApplicationUser user, Uri locationHeader)
        {
            var dto = new UserDTO()
            {
                Url = locationHeader == null ? null : locationHeader.ToString(),
                Id = user.Id,
                FullName = string.Format("{0} {1}", user.FirstName, user.LastName),
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Roles = await _userRepository.GetRolesAsync(user),
                Claims = await _userRepository.GetClaimsAsync(user)
            };

            return dto;
        }

        public async Task<ApplicationUser> RegisterAccountAsync(UserRegisterBindingModel model)
        {
            var user = new ApplicationUser();

            var roleExists = await _roleRepository.CheckIfRoleExistsAsync("User");
            var userExists = await _userRepository.CheckIfUserExistsAsync(model.Email);

            if (!roleExists)
            {
                user.IdentityErrors.Add("Role USER does not exist.");
            }

            if (roleExists && userExists)
            {
                user.IdentityErrors.Add("User already exists.");
            }

            if (user.IdentityErrors.Count == 0)
            {
                user = await _userRepository.CreateUserAsync(model);
            }

            if (user.IdentityErrors.Count == 0)
            {
                // Send confirmation token on email
                var emailConfirmationToken = await _userRepository.GenerateEmailConfirmationTokenAsync(user);

                //await _mailRepository.WelcomeCompany(user.Email, $"{user.FirstName} {user.LastName}".Trim(), emailConfimationToken);
            }

            return user;
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string authAppUser)
        {
            var user = await _userRepository.GetUserByNameAsync(authAppUser);

            return user;
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            var roles = await _userRepository.GetUserRolesAsync(user);

            return roles;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            return user;
        }

        public List<ApplicationUser> GetUsers(string searchTerm)
        {
            var users = _userRepository.GetUsers(searchTerm);

            return users;
        }

        public async Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            var users = await _userRepository.GetUsersInRoleAsync(roleName);

            return users;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            return user;
        }

        public async Task<bool> SignInUserAsync(ApplicationUser user, string password)
        {
            var signInResult = await _userRepository.CheckPasswordSignInAsync(user, password);

            return signInResult;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            var userClaims = await _userRepository.GetUserClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
            userClaims.Add(new Claim("userId", user.Id.ToString()));

            var userInRoles = await _userRepository.GetUserRolesAsync(user);

            foreach (var r in userInRoles)
            {
                userClaims.Add(new Claim("role", r));
            }

            return userClaims;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            var isInRole = await _userRepository.IsInRoleAsync(user, roleName);

            return isInRole;
        }

        public async Task<List<ApplicationUser>> ChangeUserRolesAsync(string authenticatedAppUser, UserRolesBindingModel model)
        {
            var result = new List<ApplicationUser>();

            var appUser = await _userRepository.GetUserByNameAsync(authenticatedAppUser);
            var isAppUserSuperAdmin = await _userRepository.IsInRoleAsync(appUser, "SuperAdmin");

            var user = await _userRepository.GetUserByIdAsync(model.UserId.ToString());
            var userIsSuperAdmin = await _userRepository.IsInRoleAsync(user, "SuperAdmin");
            var userInRoles = await _userRepository.GetRolesAsync(user);

            if (!(!isAppUserSuperAdmin && userIsSuperAdmin))
            {
                // Add user to roles
                model.RoleNames.ForEach(async r =>
                {
                    if (!userInRoles.Contains(r))
                    {
                        var u = await _userRepository.AddUserToRoleAsync(user, r);

                        result.Add(u);
                    }
                });

                // Remove user from roles
                model.RoleNames.ForEach(async r =>
                {
                    if (!model.RoleNames.Contains(r))
                    {
                        var u = await _userRepository.RemoveUserFromRoleAsync(user, r);

                        result.Add(u);
                    }
                });
            }

            return result;
        }

        public async Task<ApplicationUser> UpdateUser(string authenticatedAppUser, UserUpdateBindingModel model)
        {
            var user = await _userRepository.GetUserByNameAsync(authenticatedAppUser);

            if (user != null)
            {
                user.FirstName = string.IsNullOrEmpty(model.FirstName) ? user.FirstName : model.FirstName;
                user.LastName = string.IsNullOrEmpty(model.LastName) ? user.LastName : model.LastName;
                user.PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) ? user.PhoneNumber : model.PhoneNumber;
                user.Modified = DateTime.Now;

                user = await _userRepository.UpdateUserAsync(user);
            }

            return user;
        }

        public async Task<ApplicationUser> DeleteUser(string authenticatedAppUser, string id)
        {
            var appUser = await _userRepository.GetUserByNameAsync(authenticatedAppUser);
            var isAppUserSuperAdmin = await _userRepository.IsInRoleAsync(appUser, "SuperAdmin");

            var user = await _userRepository.GetUserByIdAsync(id);
            var userRoles = await _userRepository.GetRolesAsync(user);

            if (appUser.Id == user.Id)
            {
                user.IdentityErrors.Add("User is not permitted to delete himself!");
            }

            if (!isAppUserSuperAdmin && userRoles.Contains("SuperAdmin"))
            {
                user.IdentityErrors.Add("Admin is not permitted to delete users in SuperAdmin role!");
            }

            if (user.IdentityErrors.Count == 0)
            {
                user = await _userRepository.DeleteUserAsync(id);
            }

            return user;
        }

        public async Task<ApplicationUser> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            user = await _userRepository.ConfirmEmailAsync(user, token);

            return user;
        }

        public async Task<ApplicationUser> ResendEmailConfirmation(ApplicationUser user)
        {
            // Send confirmation token on email
            var emailConfirmationToken = await _userRepository.GenerateEmailConfirmationTokenAsync(user);

            //var response = await _mailRepository.WelcomeCompany(user.Email, $"{user.FirstName} {user.LastName}".Trim(), emailConfimationToken);

            if (true) // reponse
            {

            }
            else
            {
                //user.IdentityErrors.Add("IDENTITY_ERRORS", "Email confirmation is not sent.");
            }

            return user;
        }

        public async Task<ApplicationUser> ChangePassword(string authenticatedAppUser, UserPasswordChangeBindingModel model)
        {
            var user = await _userRepository.GetUserByNameAsync(authenticatedAppUser);

            user = await _userRepository.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            return user;
        }

        public async Task<ApplicationUser> RequestPasswordResetEmail(ApplicationUser user)
        {
            // Send confirmation token on email
            var emailConfirmationToken = await _userRepository.GeneratePasswordResetTokenAsync(user);

            //var response = await _mailRepository.WelcomeCompany(user.Email, $"{user.FirstName} {user.LastName}".Trim(), emailConfimationToken);

            if (true) // reponse
            {

            }
            else
            {
                //user.IdentityErrors.Add("IDENTITY_ERRORS", "Email confirmation is not sent.");
            }

            return user;
        }

        public async Task<ApplicationUser> ResetPasswordAsync(ApplicationUser user, string token, string password)
        {
            var result = await _userRepository.ResetPasswordAsync(user, token, password);

            return user;
        }
    }
}