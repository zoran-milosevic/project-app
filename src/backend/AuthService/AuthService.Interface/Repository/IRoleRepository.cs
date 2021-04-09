using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Model.Binding;
using AuthService.Model.Entities;

namespace AuthService.Interface.Repository
{
    public interface IRoleRepository
    {
        Task<bool> CheckIfRoleExistsAsync(string roleName);
        Task<ApplicationRole> CreateRoleAsync(RoleCreateBindingModel model);
        List<ApplicationRole> GetRoles();
        Task<ApplicationRole> GetRoleByIdAsync(int id);
        Task<ApplicationRole> DeleteRoleByIdAsync(int id);
    }
}