﻿using Internship.DAL.Models.Response.Posts;

namespace Internship.DAL.Models.Response.Tags
{
    public class Tag
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
