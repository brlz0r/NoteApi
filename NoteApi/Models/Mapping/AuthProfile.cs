using AutoMapper;
using NoteApi.Models.Data;
using NoteApi.Models.Entities;
using NoteApi.Models.Request;

namespace NoteApi.Models.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthRequest, AuthData>();
            CreateMap<RefreshTokenRequest, RefreshTokenData>();
            CreateMap<RefreshTokenData, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenData>();
        }
    }
}

