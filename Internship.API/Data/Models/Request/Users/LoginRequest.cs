using System.ComponentModel.DataAnnotations;

namespace Internship.API.Data.Models.Request.Users
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Заполните, пожалуйста, поле email.")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Введите email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле пароль.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string Password { get; set; }

    }
}
