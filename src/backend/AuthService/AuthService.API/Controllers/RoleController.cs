using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public RoleController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [Route("Create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateBindingModel model)
        {
            var role = await _roleService.CreateRoleAsync(model);

            if (role.IdentityErrors.Count == 0)
            {
                var locationHeader = new Uri(Url.Link("GetRole", new { id = role.Id }));

                var dto = _roleService.GetRoleReturnModel(role, locationHeader);

                return Created(locationHeader, dto);
            }
            else
            {
                role.IdentityErrors.ForEach(q =>
                {
                    ModelState.AddModelError("IDENTITY_ERRORS", q);
                });

                return ValidationProblem(ModelState);
            }
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [Route("{id:int}", Name = "GetRole")]
        public IActionResult GetRole([FromRoute] int id)
        {
            var roles = _roleService.GetRoles();

            var role = roles.FirstOrDefault(q => q.Id == id);

            if (role != null)
            {
                var locationHeader = new Uri(Url.Link("GetRole", new { id = role.Id }));

                var dto = _roleService.GetRoleReturnModel(role, locationHeader);

                return Created(locationHeader, dto);
            }
            else
            {
                ModelState.AddModelError("IDENTITY_ERRORS", "Role Not found!");

                return ValidationProblem(ModelState);
            }
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = new List<RoleDTO>();
            var rolesFromDatabase = _roleService.GetRoles();
            var authenticatedAppUser = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var appUser = await _userService.GetUserByNameAsync(authenticatedAppUser);
            var isSuperAdmin = await _userService.IsInRoleAsync(appUser, "SuperAdmin");
            var isAdmin = await _userService.IsInRoleAsync(appUser, "Admin");

            if (rolesFromDatabase.Count > 0)
            {
                rolesFromDatabase.ForEach(r =>
                {
                    if (isSuperAdmin)
                    {
                        var locationHeader = new Uri(Url.Link("GetRole", new { id = r.Id }));

                        var dto = _roleService.GetRoleReturnModel(r, locationHeader);

                        roles.Add(dto);
                    }
                    else if (isAdmin)
                    {
                        if (r.Name != "SuperAdmin")
                        {
                            var locationHeader = new Uri(Url.Link("GetRole", new { id = r.Id }));

                            var dto = _roleService.GetRoleReturnModel(r, locationHeader);

                            roles.Add(dto);
                        }
                    }
                    else
                    {
                        if (r.Name != "SuperAdmin" && r.Name != "Admin")
                        {
                            var locationHeader = new Uri(Url.Link("GetRole", new { id = r.Id }));

                            var dto = _roleService.GetRoleReturnModel(r, locationHeader);

                            roles.Add(dto);
                        }
                    }
                });

                return Ok(roles);
            }
            else
            {
                ModelState.AddModelError("IDENTITY_ERRORS", "No roles found.");

                return ValidationProblem(ModelState);
            }
        }

        [HttpPut]
        [Route("Change")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> ChangeUserRoles([FromBody] UserRolesBindingModel model)
        {
            var validator = new UserRolesValidator();
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

            var userRoles = await _userService.ChangeUserRolesAsync(authenticatedAppUser, model);

            if (userRoles.Any(q => q.IdentityErrors.Count > 0))
            {
                userRoles.ForEach(u =>
                {
                    if (u.IdentityErrors.Count > 0)
                    {
                        u.IdentityErrors.ForEach(q =>
                        {
                            ModelState.AddModelError("IDENTITY_ERRORS", q);
                        });
                    }
                });

                return ValidationProblem(ModelState);
            }
            else
            {
                return Ok(null);
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);

            if (role != null)
            {
                var deleted = await _roleService.DeleteByIdAsync(id);

                if (role.IdentityErrors.Count == 0)
                {
                    return Ok(null);
                }
                else
                {
                    role.IdentityErrors.ForEach(q =>
                    {
                        ModelState.AddModelError("IDENTITY_ERRORS", q);
                    });

                    return ValidationProblem(ModelState);
                }
            }

            return null;
        }
    }
}