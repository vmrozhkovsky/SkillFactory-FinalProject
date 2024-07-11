using Microsoft.EntityFrameworkCore;
using Internship.DAL.Models.Response.Comments;
using Internship.DAL.Models.Response.Posts;
using Internship.DAL.Models.Response.Roles;
using Internship.DAL.Models.Response.Tags;
using Internship.DAL.Models.Response.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Internship.DAL
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

