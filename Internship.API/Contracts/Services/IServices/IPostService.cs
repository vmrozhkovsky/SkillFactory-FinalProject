﻿using Internship.API.Data.Models.Request.Posts;
using Internship.API.Data.Models.Response.Posts;

namespace Internship.API.Contracts.Services.IServices
{
    public interface IPostService
    {
        Task<PostCreateRequest> CreatePost();
        Task<Guid> CreatePost(PostCreateRequest model);
        Task<PostEditRequest> EditPost(Guid Id);
        Task EditPost(PostEditRequest model, Guid Id);
        Task RemovePost(Guid id);
        Task<List<Post>> GetPosts();
        Task<Post> ShowPost(Guid id);
    }
}
