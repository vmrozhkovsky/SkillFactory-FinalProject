using System.ComponentModel.DataAnnotations;

namespace Internship.API.Data.Models.Request.Tags
{
    public class TagCreateRequest
    {
        [Required(ErrorMessage = "Заполните, пожалуйста, поле Название.")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; }
    }
}
