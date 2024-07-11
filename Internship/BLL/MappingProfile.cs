using AutoMapper;
using Internship.DAL.Models.Request.Comments;
using Internship.DAL.Models.Request.Posts;
using Internship.DAL.Models.Request.Tags;
using Internship.DAL.Models.Request.Users;
using Internship.DAL.Models.Response.Comments;
using Internship.DAL.Models.Response.Posts;
using Internship.DAL.Models.Response.Tags;
using Internship.DAL.Models.Response.Users;

namespace Internship.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));

            CreateMap<CommentCreateRequest, Comment>();
            CreateMap<CommentEditRequest, Comment>();
            CreateMap<PostCreateRequest, Post>();
            CreateMap<PostEditViewModel, Post>();
            CreateMap<TagCreateRequest, Tag>();
            CreateMap<TagEditRequest, Tag>();
            CreateMap<UserEditRequest, User>();
        }
    }
}
