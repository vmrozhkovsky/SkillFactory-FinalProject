﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Internship.BLL.Services.IServices;
using Internship.DAL.Models.Request.Posts;
using Internship.DAL.Models.Response.Users;
using Internship.DAL.Repositories.IRepositories;

namespace Internship.BLL.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _repo;
        private readonly IPostService _postService;
        private readonly ITagRepository _tagRepo;
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;

        public PostController(ITagRepository tagRepository, IPostRepository repo, IMapper mapper, IPostService postService, UserManager<User> userManager)
        {
            _tagRepo = tagRepository;
            _repo = repo;
            _mapper = mapper;
            _postService = postService;
            _userManager = userManager;
        }

        /// <summary>
        /// [Get] Метод, показывания статьи
        /// </summary>
        [Route("Post/Show")]
        [HttpGet]
        public async Task<IActionResult> ShowPost(Guid id)
        {
            var post = await _postService.ShowPost(id);
            return View(post);
        }

        /// <summary>
        /// [Get] Метод, создания статьи
        /// </summary>
        [Route("Post/Create")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreatePost()
        {
            var model = await _postService.CreatePost();

            return View(model);
        }

        /// <summary>
        /// [Post] Метод, создания статьи
        /// </summary>
        [Route("Post/Create")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreateRequest model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.AuthorId = user.Id;

            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Body))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                return View(model);
            }

            var postId = await _postService.CreatePost(model);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, редактирования статьи
        /// </summary>
        [Route("Post/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditPost(Guid id)
        {
            var model = await _postService.EditPost(id);

            return View(model);
        }

        /// <summary>
        /// [Post] Метод, редактирования статьи
        /// </summary>
        [Authorize]
        [Route("Post/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditPost(PostEditViewModel model, Guid Id)
        {
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Body))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                return View(model);
            }

            await _postService.EditPost(model, Id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, удаления статьи
        /// </summary>
        [HttpGet]
        [Route("Post/Remove")]
        public async Task<IActionResult> RemovePost(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemovePost(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] Метод, удаления статьи
        /// </summary>
        [HttpPost]
        [Route("Post/Remove")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await _postService.RemovePost(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, получения всех статей
        /// </summary>
        [HttpGet]
        [Route("Post/Get")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();

            return View(posts);
        }
    }
}
