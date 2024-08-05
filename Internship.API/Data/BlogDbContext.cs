using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Internship.API.Data.Models.Response.Comments;
using Internship.API.Data.Models.Response.Posts;
using Internship.API.Data.Models.Response.Roles;
using Internship.API.Data.Models.Response.Tags;
using Internship.API.Data.Models.Response.Users;

namespace Internship.API.Data
{
    public class BlogDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

