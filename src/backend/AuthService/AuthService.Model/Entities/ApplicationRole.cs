using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Model.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {
            IdentityErrors = new List<string>();
        }

        [NotMapped, JsonIgnore]
        public List<string> IdentityErrors { get; set; }
    }
}
