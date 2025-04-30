using System.Net;
using AutoMapper;
using Domain.Dtos.Order;
using Domain.Entitites;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class OrderService(DataContext context, IMapper mapper) : IOrderService
{
    public async Task<PagedResponse<List<GetOrderDto>>> GetAll(OrderFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var Orders = context.Orders.AsQueryable();
        if (filter.CustomerId != null)
        {
            Orders = Orders.Where(o => o.CustomerId == filter.CustomerId);
        }

        if (filter.From != null)
        {
            var now = DateTime.UtcNow.Year;
            Orders = Orders.Where(o => now - o.OrderDate.Year <= filter.From);
        }

        if (filter.To != null)
        {
            var now = DateTime.UtcNow.Year;
            Orders = Orders.Where(o => now - o.OrderDate.Year >= filter.To);
        }

        var mapped = mapper.Map<List<GetOrderDto>>(Orders);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip((filter.PagesNum - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new PagedResponse<List<GetOrderDto>>(data, filter.PagesNum, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetOrderDto>> GetById(int id)
    {
        var Order = await context.Orders.FindAsync(id);
        if (Order == null) return new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Not found");
        return mapper.Map<Response<GetOrderDto>>(Order);
    }

    public async Task<Response<GetOrderDto>> Update(int id, UpdateOrderDto updateOrderDto)
    {
        var Order = await context.Orders.FindAsync(id);
        if (Order == null) return new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Not found");

        Order.OrderDate = updateOrderDto.OrderDate;
        Order.CustomerId = updateOrderDto.CustomerId;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Error")
            : mapper.Map<Response<GetOrderDto>>(Order);
    }

    public async Task<Response<GetOrderDto>> Create(CreateOrderDto createOrderDto)
    {
        var Order = mapper.Map<Order>(createOrderDto);
        
        await context.Orders.AddAsync(Order);
        var res = await context.SaveChangesAsync();
        
        return res == 0 
            ? new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Error")
            : mapper.Map<Response<GetOrderDto>>(Order);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Order = await context.Orders.FindAsync(id);
        
        if (Order == null) return new Response<string>(HttpStatusCode.BadRequest, "Not found");
        
        context.Orders.Remove(Order);
        
        var res = await context.SaveChangesAsync();
        
        return res == 0 
            ? new Response<string>(HttpStatusCode.BadRequest,"Not found")
            : new Response<string>(HttpStatusCode.OK, "Success");
    }
}