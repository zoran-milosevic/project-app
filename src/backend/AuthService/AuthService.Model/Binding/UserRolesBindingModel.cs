using System.Collections.Generic;

namespace AuthService.Model.Binding
{
    public class UserRolesBindingModel
    {
        public int UserId { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
