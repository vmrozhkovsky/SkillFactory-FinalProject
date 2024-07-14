using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Internship.BLL.Services.IServices;
using Internship.DAL.Models.Request.Users;
using Internship.DAL.Models.Response.Roles;
using Internship.DAL.Models.Response.Users;

namespace Internship.BLL.Services
{
    public class HomeService : IHomeService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public IMapper _mapper;

        public HomeService(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task AddTestUsers()
        {
            var testUser = new RegisterRequest { UserName = "User", Email = "Test@gmail.com", Password = "123456", FirstName = "Test", LastName = "Testov" };
            var testUser2 = new RegisterRequest { UserName = "Moderator", Email = "Test2@gmail.com", Password = "123456", FirstName = "Test2", LastName = "Testov2" };
            var testUser3 = new RegisterRequest { UserName = "Admin", Email = "Test3@gmail.com", Password = "123456",  FirstName = "Test3", LastName = "Testov3" };

            var user = _mapper.Map<User>(testUser);
            var user1 = _mapper.Map<User>(testUser2);
            var user2 = _mapper.Map<User>(testUser3);

            var userRole = new Role() { Name = "Пользователь", SecurityLvl = 0 };
            var moderRole = new Role() { Name = "Модератор", SecurityLvl = 1 };
            var adminRole = new Role() { Name = "Администратор", SecurityLvl = 3 };

            await _userManager.CreateAsync(user, testUser.Password);
            await _userManager.CreateAsync(user1, testUser2.Password);
            await _userManager.CreateAsync(user2, testUser3.Password);

            await _roleManager.CreateAsync(userRole);
            await _roleManager.CreateAsync(moderRole);
            await _roleManager.CreateAsync(adminRole);

            await _userManager.AddToRoleAsync(user, userRole.Name);
            await _userManager.AddToRoleAsync(user1, moderRole.Name);
            await _userManager.AddToRoleAsync(user2, adminRole.Name);
        }
    }
}
