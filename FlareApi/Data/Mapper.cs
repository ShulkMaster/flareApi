using AutoMapper;
using FlareApi.Api.V1.Request;
using FlareApi.Api.V1.Responses;
using FlareApi.Entities;

namespace FlareApi.Data
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserInfo>();
            CreateMap<User, UserWithPassword>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}