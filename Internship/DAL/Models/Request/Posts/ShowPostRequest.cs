using Internship.DAL.Models.Request.Tags;

namespace Internship.DAL.Models.Request.Posts
{
    public class ShowPostRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string AuthorId { get; set; }
        public List<TagRequest> Tags { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
