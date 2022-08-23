using AutoMapper;
using NoteApi.Models.Data;
using NoteApi.Models.Entities;
using NoteApi.Models.Request;

namespace NoteApi.Models.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, UserData>();
            CreateMap<UserData, User>();
            CreateMap<User, UserData>();
        }
    }
}
