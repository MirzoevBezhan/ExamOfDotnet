using AutoMapper;
using Domain.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<UpdateUserDto, IdentityUser>();

        CreateMap<IdentityUser, GetUserDto>();
    }
}