using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Internship.BLL.Services.IServices;
using Internship.DAL.Models.Request.Comments;
using Internship.DAL.Models.Response.Users;
using Internship.DAL.Repositories.IRepositories;

namespace Internship.BLL.Controllers
{
    public class CommentController : Controller
    {
        private IMapper _mapper;
        private ICommentRepository _commentRepo;
        private ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CommentController> _logger;

        public CommentController(IMapper mapper, ICommentRepository commentRepo, ICommentService commentService, UserManager<User> userManager, ILogger<CommentController> logger)
        {
            _mapper = mapper;
            _commentRepo = commentRepo;
            _commentService = commentService;
            _userManager = userManager;
            _logger = logger;
        }

        // <summary>
        /// [Get] Метод, добавление комментария
        /// </summary>
        [HttpGet]
        [Route("Comment/CreateComment")]
        public IActionResult CreateComment(Guid postId)
        {
            var model = new CommentCreateRequest() { PostId = postId };
            return View(model);
        }

        // <summary>
        /// [Post] Метод, добавление комментария
        /// </summary>
        [HttpPost]
        [Route("Comment/CreateComment")]
        public async Task<IActionResult> CreateComment(CommentCreateRequest model, Guid PostId)
        {
            model.PostId = PostId;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = _commentService.CreateComment(model, new Guid(user.Id));
            _logger.LogInformation($"Пользователем {user.Id} добавлен комментарий к статье {model.PostId}.");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, редактирования коментария
        /// </summary>
        [Route("Comment/Edit")]
        [HttpGet]
        public IActionResult EditComment(Guid id)
        {
            var view = new CommentEditRequest { Id = id };
            return View(view);
        }

        /// <summary>
        /// [Post] Метод, редактирования коментария
        /// </summary>
        [Authorize]
        [Route("Comment/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentEditRequest model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditComment(model);
                _logger.LogInformation($"Внесены изменения в комментарий {model.Id}.");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                _logger.LogWarning($"Возникла ошибка при изменении комментария {model.Id}");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, удаления коментария
        /// </summary>
        [HttpGet]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveComment(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Delete] Метод, удаления коментария
        /// </summary>
        [HttpDelete]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);
            _logger.LogInformation($"Удален комментарий {id}.");
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// [Get] Метод, получения всех комментариев
        /// </summary>
        [Route("Comment/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentService.GetComments();
            _logger.LogInformation($"Запрос на вывод всех комментариев обработан.");
            return View(comments);
        }
        
        /// <summary>
        /// [Get] Метод, получения комментария по его id
        /// </summary>
        [Route("Comment/Getbyid")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var comment = await _commentService.GetCommentById(id);
            _logger.LogInformation($"Запрос на вывод комментариея {id} обработан.");
            return View(comment);
        }
    }
}
