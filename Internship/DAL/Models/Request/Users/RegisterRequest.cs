﻿using System.ComponentModel.DataAnnotations;

namespace Internship.DAL.Models.Request.Users
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Заполните, пожалуйста, поле Имя.")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле Фамилия.")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле Никнейм.")]
        [DataType(DataType.Text)]
        [Display(Name = "Никнейм", Prompt = "Введите Никнейм")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле Email.")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "example.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле Пароль.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обязательно подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль", Prompt = "Введите пароль повторно")]
        public string PasswordReg { get; set; }
    }
}
