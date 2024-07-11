﻿using Internship.DAL.Models.Request.Tags;
using Internship.DAL.Models.Response.Tags;

namespace Internship.BLL.Services.IServices
{
    public interface ITagService
    {
        Task<Guid> CreateTag(TagCreateRequest model);
        Task EditTag(TagEditRequest model);
        Task RemoveTag(Guid id);
        Task<List<Tag>> GetTags();
    }
}
