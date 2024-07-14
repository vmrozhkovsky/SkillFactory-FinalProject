using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Internship.BLL.Services.IServices;
using Internship.DAL.Models.Request.Users;
using Internship.DAL.Models.Response.Roles;
using Internship.DAL.Models.Response.Users;
using Microsoft.EntityFrameworkCore;

namespace Internship.BLL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IAccountService accountService, ILogger<AccountController> logger)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// [Get] Метод, login
        /// </summary>
        [Route("Account/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// [Post] Метод, login
        /// </summary>
        [Route("Account/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var user = _mapper.Map<User>(model);
            User signedUser = _userManager.Users.Include(x => x.Roles).FirstOrDefault(u => u.Email == user.Email);
            var userRole = _userManager.GetRolesAsync(signedUser).Result.FirstOrDefault();
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(model);

                if (result.Succeeded)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)
                    };
                    await _signInManager.SignInWithClaimsAsync(signedUser, isPersistent: false, claims);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        /// <summary>
        /// [Get] Метод, регистрации
        /// </summary>
        [Route("Account/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// [Post] Метод, регистрации
        /// </summary>
        [Route("Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Register(model);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Создан аккаунт - {model.Email}");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// [Get] Метод, редактирования
        /// </summary>
        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditAccount(Guid id)
        {
            var model = await _accountService.EditAccount(id);
            return View(model);
        }

        /// <summary>
        /// [Post] Метод, редактирования
        /// </summary>
        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditAccount(UserEditRequest model)
        {
            var result = await _accountService.EditAccount(model);

            if (result.Succeeded)
            {
                _logger.LogDebug($"Аккаунт - {model.UserName} был изменен");
                return RedirectToAction("Index", "Home");
            }    
            else
            {
                ModelState.AddModelError("", $"{result.Errors.First().Description}");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, удаление аккаунта
        /// </summary>
        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveAccount(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveAccount(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] Метод, удаление аккаунта
        /// </summary>
        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveAccount(Guid id)
        {
            await _accountService.RemoveAccount(id);
            _logger.LogDebug($"Remove account {id}");

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] Метод, выхода из аккаунта
        /// </summary>
        [Route("Account/Logout")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LogoutAccount(Guid id)
        {
            await _accountService.LogoutAccount();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, получения всех пользователей
        /// </summary>
        [Route("Account/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var users = await _accountService.GetAccounts();

            return View(users);
        }
        
        /// <summary>
        /// [Get] Метод, получения пользователя по Id
        /// </summary>
        [Route("Account/Getbyid")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var user = await _accountService.GetAccountById(id);

            return View(user);
        }
    }
}
