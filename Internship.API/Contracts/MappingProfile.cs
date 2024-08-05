using AutoMapper;
using Internship.API.Data.Models.Request.Comments;
using Internship.API.Data.Models.Request.Posts;
using Internship.API.Data.Models.Request.Tags;
using Internship.API.Data.Models.Request.Users;
using Internship.API.Data.Models.Response.Comments;
using Internship.API.Data.Models.Response.Posts;
using Internship.API.Data.Models.Response.Tags;
using Internship.API.Data.Models.Response.Users;

namespace Internship.API.Contracts
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
            CreateMap<PostEditRequest, Post>();
            CreateMap<TagCreateRequest, Tag>();
            CreateMap<TagEditRequest, Tag>();
            CreateMap<UserEditRequest, User>();
        }
    }
}
