using Internship.API.Data.Models.Request.Comments;
using Internship.API.Data.Models.Response.Comments;

namespace Internship.API.Contracts.Services.IServices
{
    public interface ICommentService
    {
        Task<Guid> CreateComment(CommentCreateRequest model);
        Task<int> EditComment(CommentEditRequest model);
        Task RemoveComment(Guid id);
        Task<List<Comment>> GetComments();
    }
}
