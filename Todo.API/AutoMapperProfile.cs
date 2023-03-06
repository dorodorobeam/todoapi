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
            CreateMap<User, UserModel>().ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<User, UserModel>().ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<TodoList, TodoModel>().ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id));
            CreateMap<TodoList, TodoModel>().ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id)).ReverseMap();
        }
    }
}