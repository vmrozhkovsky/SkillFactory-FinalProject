using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Identity;
using Internship.BLL.Services.IServices;
using Internship.DAL.Models.Request.Roles;
using Internship.DAL.Models.Response.Roles;
using Internship.DAL.Models.Response.Tags;
using System.Data;

namespace Internship.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private IMapper _mapper;

        public RoleService(IMapper mapper, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Guid> CreateRole(RoleCreateRequest model)
        {
            var role = new Role() {Name = model.Name, SecurityLvl = model.SecurityLvl};
            await _roleManager.CreateAsync(role);

            return Guid.Parse(role.Id);
        }

        public async Task EditRole(RoleEditRequest model)
        {
            if (string.IsNullOrEmpty(model.Name) && model.SecurityLvl == null)
                return;

            var role = await _roleManager.FindByIdAsync(model.Id.ToString());

            if (!string.IsNullOrEmpty(model.Name))
                role.Name = model.Name;
            if (model.SecurityLvl != null)
                role.SecurityLvl = model.SecurityLvl;

            await _roleManager.UpdateAsync(role);
        }

        public async Task RemoveRole(Guid Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            await _roleManager.DeleteAsync(role);
        }

        public async Task<List<Role>> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }
    }
}
