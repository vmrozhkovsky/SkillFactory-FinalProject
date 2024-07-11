using Microsoft.AspNetCore.Identity;
using Internship.DAL.Models.Response.Posts;
using Internship.DAL.Models.Response.Roles;

namespace Internship.DAL.Models.Response.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedData { get; set; } = DateTime.Now;

        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
