using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Model.Binding;
using AuthService.Model.DTO;
using AuthService.Model.Entities;

namespace AuthService.Interface.Service
{
    public interface IRoleService
    {
        RoleDTO GetRoleReturnModel(ApplicationRole role, Uri locationHeader);
        Task<ApplicationRole> CreateRoleAsync(RoleCreateBindingModel model);
        List<ApplicationRole> GetRoles();
        Task<ApplicationRole> GetRoleByIdAsync(int id);
        Task<ApplicationRole> DeleteByIdAsync(int id);
    }
}