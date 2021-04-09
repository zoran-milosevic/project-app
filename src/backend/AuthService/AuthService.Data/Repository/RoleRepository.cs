using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AuthService.Interface.Repository;
using AuthService.Model.Binding;
using AuthService.Model.Entities;

namespace AuthService.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CheckIfRoleExistsAsync(string roleName)
        {
            var role = await _roleManager.RoleExistsAsync(roleName);

            return role;
        }

        public async Task<ApplicationRole> CreateRoleAsync(RoleCreateBindingModel model)
        {
            var role = new ApplicationRole()
            {
                Name = model.Name
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                role.IdentityErrors = result.Errors.Select(q => q.Description).ToList();
            }
            else
            {
                role = await _roleManager.FindByNameAsync(model.Name);
            }

            return role;
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            return role;
        }

        public List<ApplicationRole> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();

            return roles;
        }

        public async Task<ApplicationRole> DeleteRoleByIdAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                role.IdentityErrors = result.Errors.Select(q => q.Description).ToList();
            }

            return role;
        }
    }
}