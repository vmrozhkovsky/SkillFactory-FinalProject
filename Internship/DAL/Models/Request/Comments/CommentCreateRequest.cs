using System.ComponentModel.DataAnnotations;

namespace Internship.DAL.Models.Request.Comments
{
    public class CommentCreateRequest
    {
        [Required(ErrorMessage = "Заполните, пожалуйста, поле Заголовок.")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле Описание.")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле Автор.")]
        [DataType(DataType.Text)]
        [Display(Name = "Автор", Prompt = "Автор")]
        public string Author { get; set; }

        public Guid PostId;
    }
}
