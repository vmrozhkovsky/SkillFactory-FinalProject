using System.ComponentModel.DataAnnotations;

namespace Internship.API.Data.Models.Request.Tags
{
    public class TagEditRequest
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; }
    }
}
