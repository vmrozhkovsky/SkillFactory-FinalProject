using Internship.DAL.Models.Request.Comments;
using Internship.DAL.Models.Response.Comments;

namespace Internship.BLL.Services.IServices
{
    public interface ICommentService
    {
        Task<Guid> CreateComment(CommentCreateRequest model, Guid UserId);
        Task EditComment(CommentEditRequest model);
        Task RemoveComment(Guid id);
        Task<List<Comment>> GetComments();
        Task<Comment> GetCommentById(Guid id);
    }
}
