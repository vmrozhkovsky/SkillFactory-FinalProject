using System.ComponentModel.DataAnnotations;
using Internship.DAL.Models.Request.Tags;
using Internship.DAL.Models.Response.Tags;
using Internship.DAL.Repositories;
using Internship.DAL.Repositories.IRepositories;

namespace Internship.DAL.Models.Request.Posts
{
    public class PostCreateRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
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
