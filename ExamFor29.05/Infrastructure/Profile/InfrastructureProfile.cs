using AutoMapper;
using Domain.DTOs.User;
using Domain.DTOs.User.Product;
using Domain.Entities;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<CreateUserDto, IdentityUser>();
        CreateMap<CreateProductDto, Product>();
        
        CreateMap<GetUserDto, Response<GetUserDto>>();
        
        CreateMap<IdentityUser, GetUserDto>();
        CreateMap<Product, GetProductDto>();
    }
}