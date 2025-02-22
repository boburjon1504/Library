using AutoMapper;
using Library.API.DTOs;
using Library.Models.Entities;

namespace Library.API.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDTO>()
                                .ReverseMap();
    }
}
