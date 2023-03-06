using AutoMapper;
using Todo.Application.CQRS.Todo.Models;
using Todo.Application.CQRS.User.Models;
using Todo.Domain.Entities;

namespace Todo.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<TodoList, TodoModel>().ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id));
            CreateMap<TodoList, TodoModel>().ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id)).ReverseMap();
            //CreateMap<TodoModel, TodoList>().ForMember(x => x.User, opt => opt.MapFrom(src => src)).ReverseMap();
        }
    }
}