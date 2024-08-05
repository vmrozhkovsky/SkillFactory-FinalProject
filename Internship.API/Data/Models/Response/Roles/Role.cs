using Microsoft.AspNetCore.Identity;

namespace Internship.API.Data.Models.Response.Roles
{
    public class Role : IdentityRole
    {
        public int? SecurityLvl { get; set; } = null;
    }
}
