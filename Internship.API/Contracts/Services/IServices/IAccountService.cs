﻿using Microsoft.AspNetCore.Identity;
using Internship.API.Data.Models.Request.Users;
using Internship.API.Data.Models.Response.Users;

namespace Internship.API.Contracts.Services.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterRequest model);
        Task<IdentityResult> EditAccount(UserEditRequest model);
        Task<SignInResult> Login(LoginRequest model);
        Task<UserEditRequest> EditAccount(Guid id);
        Task RemoveAccount(Guid id);
        Task<List<User>> GetAccounts();
    }
}
