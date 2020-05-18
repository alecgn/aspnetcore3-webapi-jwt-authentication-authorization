using AutoMapper;
using AspNetCore3_WebAPI_JWT.DTOs;
using AspNetCore3_WebAPI_JWT.Models;

namespace AspNetCore3_WebAPI_JWT
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
