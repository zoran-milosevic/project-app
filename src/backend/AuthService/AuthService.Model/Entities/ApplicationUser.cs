using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Model.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            IdentityErrors = new List<string>();
            Roles = new List<string>();
            Claims = new List<Claim>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        [NotMapped, JsonIgnore]
        public List<string> Roles { get; set; }

        [NotMapped, JsonIgnore]
        public List<Claim> Claims { get; set; }

        [NotMapped, JsonIgnore]
        public List<string> IdentityErrors { get; set; }
    }
}
