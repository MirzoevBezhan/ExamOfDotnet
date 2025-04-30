using Domain.Dtos.Customer;
using Domain.Dtos.Order;
using Domain.Dtos.OrderItem;
using Domain.Dtos.Product;
using Domain.Entitites;

namespace Infrastructure.Profile;

public class InfrastructureProfile : AutoMapper.Profile
{
    public InfrastructureProfile()
    {
        CreateMap<CreateOrderDto, Order>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<CreateOrderItemDto, OrderItem>();
        CreateMap<UpdateCustomerDto, Customer>();

        
        CreateMap<OrderItem, GetOrderItemDto>();
        CreateMap<Order, GetOrderDto>();
        CreateMap<Product, GetProductDto>();
        CreateMap<Customer, GetCustomerDto>();
    }
}