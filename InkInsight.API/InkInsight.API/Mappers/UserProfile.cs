using AutoMapper;
using InkInsight.API.Dto;
using InkInsight.API.Entities;

namespace InkInsight.API.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
