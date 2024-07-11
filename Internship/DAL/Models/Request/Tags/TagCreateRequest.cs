using System.ComponentModel.DataAnnotations;

namespace Internship.DAL.Models.Request.Tags
{
    public class TagCreateRequest
    {
        [Required(ErrorMessage = "Заполните, пожалуйста, поле Название.")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; }
    }
}
