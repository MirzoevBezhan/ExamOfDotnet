using AutoMapper;
using Domain.Dtos.Branche;
using Domain.Dtos.Car;
using Domain.Dtos.Customer;
using Domain.Dtos.Rental;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<CreateCarDto, Car>();
        CreateMap<CreateBranchDto, Branch>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<CreateRentalDto, Rental>();
        CreateMap<CreateUserDto, IdentityUser>();
        
        CreateMap<GetUserDto, Response<GetUserDto>>();
        
        CreateMap<IdentityUser, GetUserDto>();
        CreateMap<Car, GetCarDto>();
        CreateMap<Rental, GetRentalDto>();
        CreateMap<Branch, GetBranchDto>();
        CreateMap<Customer, GetCustomerDto>();
    }
}