using System.Net;
using AutoMapper;
using Domain.Dtos.OrderItem;
using Domain.Entitites;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class OrderItemService(DataContext context, IMapper mapper) : IOrderItemService
{
    public async Task<PagedResponse<List<GetOrderItemDto>>> GetAll(OrderItemFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var OrderItems = context.OrderItems.AsQueryable();
        if (filter.From != null)
        {
            OrderItems = OrderItems.Where(o => o.Quantity <= filter.From);
        }

        if (filter.To != null)
        {
            OrderItems = OrderItems.Where(o => o.Quantity >= filter.To);
        }

        var mapped = mapper.Map<List<GetOrderItemDto>>(OrderItems);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip((filter.PagesNum - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new PagedResponse<List<GetOrderItemDto>>(data, filter.PagesNum, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetOrderItemDto>> GetById(int id)
    {
        var OrderItem = await context.OrderItems.FindAsync(id);
        if (OrderItem == null) return new Response<GetOrderItemDto>(HttpStatusCode.BadRequest, "Not found");
        return mapper.Map<Response<GetOrderItemDto>>(OrderItem);
    }

    public async Task<Response<GetOrderItemDto>> Update(int id, UpdateOrderItemDto updateOrderItemDto)
    {
        var OrderItem = await context.OrderItems.FindAsync(id);
        if (OrderItem == null) return new Response<GetOrderItemDto>(HttpStatusCode.BadRequest, "Not found");

        OrderItem.Quantity = updateOrderItemDto.Quantity;
        OrderItem.ProductId = updateOrderItemDto.ProductId;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetOrderItemDto>(HttpStatusCode.BadRequest, "Error")
            : mapper.Map<Response<GetOrderItemDto>>(OrderItem);
    }

    public async Task<Response<GetOrderItemDto>> Create(CreateOrderItemDto createOrderItemDto)
    {
        var OrderItem = mapper.Map<OrderItem>(createOrderItemDto);

        var Order = context.Orders.FirstOrDefault(o => o.Id == OrderItem.OrderId);
        if (Order == null)
        {
            return new Response<GetOrderItemDto>(HttpStatusCode.BadRequest, "Not found Order");
        }
        var Product = context.Products.FirstOrDefault(o => o.Id == OrderItem.ProductId);
        if (Product == null)
        {
            return new Response<GetOrderItemDto>(HttpStatusCode.BadRequest, "Not found Product");
        }

        await context.OrderItems.AddAsync(OrderItem);
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetOrderItemDto>(HttpStatusCode.BadRequest, "Error")
            : mapper.Map<Response<GetOrderItemDto>>(OrderItem);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var OrderItem = await context.OrderItems.FindAsync(id);

        if (OrderItem == null) return new Response<string>(HttpStatusCode.BadRequest, "Not found");

        context.OrderItems.Remove(OrderItem);

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Not found")
            : new Response<string>(HttpStatusCode.OK, "Success");
    }
}