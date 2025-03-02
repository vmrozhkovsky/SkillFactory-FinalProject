﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Internship.API.Contracts;
using Internship.API.Contracts.Services;
using Internship.API.Contracts.Services.IServices;
using Internship.API.Data;
using Internship.API.Data.Models.Response.Roles;
using Internship.API.Data.Models.Response.Users;
using Internship.API.Data.Repositories;
using Internship.API.Data.Repositories.IRepositories;

namespace Internship.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var filepath = Path.Combine(AppContext.BaseDirectory, "Internship.API.xml");
                c.IncludeXmlComments(filepath);
            });

            // Connect AutoMapper
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            // Connect DataBase
            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connection))
                .AddIdentity<User, Role>(opts =>
                {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<BlogDbContext>();

            // Services AddSingletons/Transient
            builder.Services
                .AddSingleton(mapper)
                .AddTransient<ICommentRepository, CommentRepository>()
                .AddTransient<ITagRepository, TagRepository>()
                .AddTransient<IPostRepository, PostRepository>()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<IPostService, PostService>()
                .AddTransient<ITagService, TagService>()
                .AddTransient<IRoleService, RoleService>();

            builder.Services.AddAuthentication(optionts => optionts.DefaultScheme = "Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}