using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthService.Interface.Service;
using AuthService.Model.Binding;
using AuthService.Model.DTO;
using AuthService.Api.Validations;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json"), Produces("application/json")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public TokenController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("generate")]
        public async Task<IActionResult> GenerateToken([FromBody] UserLoginBindingModel model)
        {
            var validator = new UserLoginValidator();
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

            if (user.EmailConfirmed == false)
            {
                ModelState.AddModelError("IDENTITY_ERRORS", "User's email address is not confirmed.");

                return ValidationProblem(ModelState);
            }

            if (user.IdentityErrors.Count == 0)
            {
                var signInResult = await _userService.SignInUserAsync(user, model.Password);

                if (signInResult == true)
                {
                    var token = await _tokenService.GetToken(user);

                    return Ok(new TokenDTO { AccessToken = token });
                }
                else
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", "User's email address or password is incorrect.");

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
        [Route("refresh")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshBindingModel model)
        {
            var validator = new TokenRefreshValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(q =>
                {
                    ModelState.AddModelError("MODEL_VALIDATION_ERRORS", q.ErrorMessage);
                });

                return ValidationProblem(ModelState);
            }

            var oldToken = new JwtSecurityTokenHandler().ReadToken(model.OldToken);
            var userFromToken = new JwtSecurityTokenHandler().ReadJwtToken(model.OldToken).Claims.First(claim => claim.Type == "sub").Value;

            var authenticatedAppUser = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            if (DateTime.Compare(oldToken.ValidTo, DateTime.UtcNow) == -1)
            {
                ModelState.AddModelError("IDENTITY_ERRORS", "User's JWT token expired.");

                return ValidationProblem(ModelState);
            }

            if (userFromToken == authenticatedAppUser)
            {
                var user = await _userService.GetUserByEmailAsync(userFromToken);

                if (user.IdentityErrors.Count == 0)
                {
                    var token = await _tokenService.GetToken(user);

                    return Ok(new TokenDTO { AccessToken = token });
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
                ModelState.AddModelError("IDENTITY_ERRORS", "Authorization error!");

                return ValidationProblem(ModelState);
            }
        }
    }
}