using Domain.Dtos.Customer;
using Domain.Dtos.Reservation;
using Domain.Dtos.Table;
using Domain.Entities;
using Domain.Responces;

namespace Infrastructure.Profile;

public class InfrastructureProfile : AutoMapper.Profile
{
    public InfrastructureProfile()
    {
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<CreateTableDto, Table>();
        CreateMap<CreateReservationDto, Reservation>();

        CreateMap<Table, GetTableDto>();
        CreateMap<Reservation, GetReservationDto>();
        CreateMap<Customer, GetCustomerDto>();
    }
}