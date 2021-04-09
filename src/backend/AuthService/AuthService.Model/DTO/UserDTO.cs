using System.Collections.Generic;

namespace AuthService.Model.DTO
{
    public class UserDTO
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; }
        public List<System.Security.Claims.Claim> Claims { get; set; }
    }
}
