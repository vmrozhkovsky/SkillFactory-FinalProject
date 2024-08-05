using System.ComponentModel.DataAnnotations;

namespace Internship.DAL.Models.Request.Roles
{
    public class RoleCreateRequest
    {
        [Required(ErrorMessage = "Заполните, пожалуйста, поле Название.")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле Уровень доступа.")]
        [DataType(DataType.Text)]
        [Display(Name = "Уровень доступа", Prompt = "Уровень")]
        public int SecurityLvl { get; set; }
    }
}
