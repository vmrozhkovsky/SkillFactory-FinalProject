using System.ComponentModel.DataAnnotations;
using Internship.API.Data.Models.Request.Tags;

namespace Internship.API.Data.Models.Request.Posts
{
    public class PostCreateRequest
    {
        public string AuthorId { get; set; }
        public List<TagRequest> Tags { get; set; }


        [Required(ErrorMessage = "Заполните, пожалуйста, поле Заголовок.")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Заполните, пожалуйста, поле Описание.")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Описание")]
        public string Body { get; set; }
    }
}
