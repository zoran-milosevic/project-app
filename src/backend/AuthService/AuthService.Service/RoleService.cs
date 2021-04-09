using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Interface.Repository;
using AuthService.Interface.Service;
using AuthService.Model.Binding;
using AuthService.Model.DTO;
using AuthService.Model.Entities;

namespace AuthService.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public RoleDTO GetRoleReturnModel(ApplicationRole role, Uri locationHeader)
        {
            var roleReturnModel = new RoleDTO()
            {
                Url = locationHeader == null ? null : locationHeader.ToString(),
                Id = role.Id,
                Name = role.Name
            };

            return roleReturnModel;
        }

        public async Task<ApplicationRole> CreateRoleAsync(RoleCreateBindingModel model)
        {
            var role = new ApplicationRole();

            var roleExists = await _roleRepository.CheckIfRoleExistsAsync(model.Name);

            if (roleExists)
            {
                role.IdentityErrors.Add("Role already exists.");
            }

            if (role.IdentityErrors.Count == 0)
            {
                role = await _roleRepository.CreateRoleAsync(model);
            }

            return role;
        }

        public List<ApplicationRole> GetRoles()
        {
            var roles = _roleRepository.GetRoles();

            return roles;
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);

            return role;
        }

        public async Task<ApplicationRole> DeleteByIdAsync(int id)
        {
            var result = await _roleRepository.DeleteRoleByIdAsync(id);

            return result;
        }
    }

}