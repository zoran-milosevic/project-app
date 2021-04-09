using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthService.Api.Validations;
using AuthService.Interface.Service;
using AuthService.Model.Binding;
using AuthService.Model.DTO;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json"), Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterBindingModel model)
        {
            var validator = new UserRegisterValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(q =>
                {
                    ModelState.AddModelError("MODEL_VALIDATION_ERRORS", q.ErrorMessage);
                });

                return ValidationProblem(ModelState);
            }

            var user = await _userService.RegisterAccountAsync(model);

            if (user.IdentityErrors.Count == 0)
            {
                var locationHeader = new Uri(Url.Link("GetUser", new { id = user.Id }));

                var dto = await _userService.GetUserReturnModel(user, locationHeader);

                return Created(locationHeader, dto);
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpGet]
        [Route("Profile")]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            var authAppUser = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _userService.GetUserByNameAsync(authAppUser);

            if (user.IdentityErrors.Count == 0)
            {
                var locationHeader = new Uri(Url.Link("GetUser", new { id = user.Id }));

                var dto = await _userService.GetUserReturnModel(user, locationHeader);

                return Created(locationHeader, dto);
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetUser")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var authAppUser = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var appUser = await _userService.GetUserByNameAsync(authAppUser);
            var user = await _userService.GetUserByIdAsync(id);

            appUser.Roles = await _userService.GetUserRolesAsync(appUser);
            user.Roles = await _userService.GetUserRolesAsync(user);

            if (user.IdentityErrors.Count == 0)
            {
                if (appUser.Roles.Contains("SuperAdmin") || appUser.Roles.Contains("Admin"))
                {
                    var locationHeader = new Uri(Url.Link("GetUser", new { id = user.Id }));

                    var dto = await _userService.GetUserReturnModel(user, locationHeader);

                    return Created(locationHeader, dto);
                }
                else
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", "User is not allowed to get a user's profile if a user is not in Admin role.");

                    return ValidationProblem(ModelState);
                }
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpGet]
        [Route("Search/{searchTerm?}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> GetUsers([FromRoute] string searchTerm = "")
        {
            var authAppUser = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var users = new List<UserDTO>();
            var userList = _userService.GetUsers(searchTerm);
            var appUser = await _userService.GetUserByNameAsync(authAppUser);
            var superAdminUsers = await _userService.GetUsersInRoleAsync("SuperAdmin");

            appUser.Roles = await _userService.GetUserRolesAsync(appUser);

            if (appUser.IdentityErrors.Count == 0)
            {
                foreach (var u in userList)
                {
                    if (appUser.Roles.Contains("SuperAdmin"))
                    {
                        users.Add(await _userService.GetUserReturnModel(u, null));
                    }
                    else
                    {
                        if (!superAdminUsers.Contains(u))
                        {
                            users.Add(await _userService.GetUserReturnModel(u, null));
                        }
                    }
                }

                return Ok(users);
            }
            else
            {
                appUser.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserUpdateBindingModel model)
        {
            var authenticatedAppUser = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _userService.UpdateUser(authenticatedAppUser, model);

            if (user.IdentityErrors.Count == 0)
            {
                var locationHeader = new Uri(Url.Link("GetUser", new { id = user.Id }));

                var dto = await _userService.GetUserReturnModel(user, locationHeader);

                return Ok(dto);
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var authenticatedAppUser = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _userService.DeleteUser(authenticatedAppUser, id);

            if (user.IdentityErrors.Count == 0)
            {
                return Ok(null);
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email = "", string token = "")
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("IDENTITY_ERRORS", "Email address or token can't be an empty string!");

                return ValidationProblem(ModelState);
            }

            var user = await _userService.GetUserByEmailAsync(email);

            if (user.IdentityErrors.Count == 0)
            {
                user = await _userService.ConfirmEmailAsync(user, token);

                if (user.IdentityErrors.Count == 0)
                {
                    return Ok(null);
                }
                else
                {
                    user.IdentityErrors.ForEach(q =>
                    {
                        ModelState.AddModelError("IDENTITY_ERRORS", q);
                    });

                    return ValidationProblem(ModelState);
                }
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpGet]
        [Route("ResendEmailConfirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendEmailConfirmation([FromQuery] string email = "")
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("IDENTITY_ERRORS", "Email address can't be an empty string!");

                return ValidationProblem(ModelState);
            }

            var user = await _userService.GetUserByEmailAsync(email);

            if (user.IdentityErrors.Count == 0)
            {
                user = await _userService.ResendEmailConfirmation(user);

                if (user.IdentityErrors.Count == 0)
                {
                    return Ok(null);
                }
                else
                {
                    user.IdentityErrors.ForEach(q =>
                    {
                        ModelState.AddModelError("IDENTITY_ERRORS", q);
                    });

                    return ValidationProblem(ModelState);
                }
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserPasswordChangeBindingModel model)
        {
            var validator = new UserPasswordChangeValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(q =>
                {
                    ModelState.AddModelError("MODEL_VALIDATION_ERRORS", q.ErrorMessage);
                });

                return ValidationProblem(ModelState);
            }

            var authenticatedAppUser = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _userService.ChangePassword(authenticatedAppUser, model);

            if (user.IdentityErrors.Count == 0)
            {
                return Ok(null);
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("RequestPasswordResetEmail")]
        public async Task<IActionResult> RequestPasswordResetEmail([FromQuery] string email = "")
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("IDENTITY_ERRORS", "Email address can't be an empty string!");

                return ValidationProblem(ModelState);
            }

            var user = await _userService.GetUserByEmailAsync(email);

            if (user.IdentityErrors.Count == 0)
            {
                user = await _userService.RequestPasswordResetEmail(user);

                if (user.IdentityErrors.Count == 0)
                {
                    return Ok(null);
                }
                else
                {
                    user.IdentityErrors.ForEach(q =>
                    {
                        ModelState.AddModelError("IDENTITY_ERRORS", q);
                    });

                    return ValidationProblem(ModelState);
                }
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserPasswordResetBindingModel model)
        {
            var validator = new UserPasswordResetValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(q =>
                {
                    ModelState.AddModelError("MODEL_VALIDATION_ERRORS", q.ErrorMessage);
                });

                return ValidationProblem(ModelState);
            }

            var user = await _userService.GetUserByEmailAsync(model.Email);

            if (user.IdentityErrors.Count == 0)
            {
                user = await _userService.ResetPasswordAsync(user, model.Token, model.Password);

                if (user.IdentityErrors.Count == 0)
                {
                    return Ok(null);
                }
                else
                {
                    user.IdentityErrors.ForEach(q =>
                    {
                        ModelState.AddModelError("IDENTITY_ERRORS", q);
                    });

                    return ValidationProblem(ModelState);
                }
            }
            else
            {
                user.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }
    }
}
