using Internship.API.Data.Models.Request.Tags;
using Internship.API.Data.Models.Response.Tags;

namespace Internship.API.Contracts.Services.IServices
{
    public interface ITagService
    {
        Task<Guid> CreateTag(TagCreateRequest model);
        Task EditTag(TagEditRequest model);
        Task RemoveTag(Guid id);
        Task<List<Tag>> GetTags();
    }
}
