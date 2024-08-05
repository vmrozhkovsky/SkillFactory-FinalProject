using Internship.API.Data.Models.Request.Roles;
using Internship.API.Data.Models.Response.Roles;

namespace Internship.API.Contracts.Services.IServices
{
    public interface IRoleService
    {
        Task<Guid> CreateRole(RoleCreateRequest model);
        Task EditRole(RoleEditRequest model);
        Task RemoveRole(Guid id);
        Task<List<Role>> GetRoles();
    }
}
