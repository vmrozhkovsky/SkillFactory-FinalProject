using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Internship.BLL.Services.IServices;
using Internship.DAL.Models.Request.Roles;

namespace Internship.BLL.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;
        private IMapper _mapper;
        public RoleController(IMapper mapper, IRoleService roleService, ILogger<RoleController> logger)
        {
            _mapper = mapper;
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// [Get] Метод, создания роли
        /// </summary>
        [Route("Role/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        /// <summary>
        /// [Post] Метод, создания роли
        /// </summary>
        [Route("Role/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateRequest model)
        {
            if (ModelState.IsValid)
            {
                var roleId = await _roleService.CreateRole(model);
                _logger.LogDebug($"Создана роль - {model.Name}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                _logger.LogWarning($"Возникла ошибка при создании роли {model.Name}.");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, редактирования роли
        /// </summary>
        [Route("Role/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult EditRole(Guid id)
        {
            var view = new RoleEditRequest { Id = id };
            return View(view);
        }

        /// <summary>
        /// [Post] Метод, редактирования роли
        /// </summary>
        [Route("Role/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditRequest model)
        {
            if (ModelState.IsValid)
            {
                await _roleService.EditRole(model);
                _logger.LogDebug($"Изменена роль - {model.Name}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                _logger.LogWarning($"Возникла ошибка при редактировании роли {model.Name}.");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, удаления роли
        /// </summary>
        [Route("Role/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveRole(Guid id, bool isConfirm = true)
        {
            if (isConfirm)
                await RemoveRole(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] Метод, удаления роли
        /// </summary>
        [Route("Role/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            await _roleService.RemoveRole(id);
            _logger.LogDebug($"Удалена роль - {id}");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, получения всех ролей
        /// </summary>
        [Route("Role/GetRoles")]
        [HttpGet]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            _logger.LogInformation("Запрос на вывод всех ролей обработан.");
            return View(roles);
        }
    }
}
