using System.Net;
using AutoMapper;
using Domain.DTOs.StockAdjustment;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StockAdjustmentService(DataContext context,IMapper mapper) : IStockAdjustmentService
{
    public async Task<Response<GetStockAdjustmentDto>> Add(CreateStockAdjustmentDto create)
    {
        var StockAdjustment = mapper.Map<StockAdjustment>(create);

        await context.StockAdjustments.AddAsync(StockAdjustment);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new Response<GetStockAdjustmentDto>(HttpStatusCode.BadRequest, "Don't Added")
            : mapper.Map<Response<GetStockAdjustmentDto>>(StockAdjustment);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var StockAdjustment = await context.StockAdjustments.FindAsync(id);

        if (StockAdjustment == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }

        context.StockAdjustments.Remove(StockAdjustment);
        var res = await context.SaveChangesAsync();

        return new Response<string>("Deleted");
    }

    public async Task<Response<GetStockAdjustmentDto>> Get(int id)
    {
        var StockAdjustment = await context.StockAdjustments.FindAsync(id);
        return StockAdjustment == null
            ? new Response<GetStockAdjustmentDto>(HttpStatusCode.BadRequest, "Not Found")
            : mapper.Map<Response<GetStockAdjustmentDto>>(StockAdjustment);
    }

    public async Task<PagedResponse<List<GetStockAdjustmentDto>>> GetAll(StockAdjustmentFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var StockAdjustments = context.StockAdjustments.AsQueryable();

        if (filter.Reason != null)
        {
            StockAdjustments = StockAdjustments.Where(c => c.Reason.Contains(filter.Reason));
        }

        if (filter.From != null)
        {
            var now = DateTime.Now.Year;
            StockAdjustments = StockAdjustments.Where(s => now - s.AdjustmentDate.Year >= filter.From);
        }

        if (filter.To != null)
        {
            var now = DateTime.Now.Year;
            StockAdjustments = StockAdjustments.Where(s => now - s.AdjustmentDate.Year <= filter.To);
        } 

        var mapped = mapper.Map<List<GetStockAdjustmentDto>>(StockAdjustments);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageSize * (validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize);

        return mapper.Map<PagedResponse<List<GetStockAdjustmentDto>>>(data);
    }

    public async Task<Response<GetStockAdjustmentDto>> Update(int id, UpdateStockAdjustmentDto update)
    {
        var StockAdjustment = await context.StockAdjustments.FindAsync(id);

        if (StockAdjustment == null)
        {
            return new Response<GetStockAdjustmentDto>(HttpStatusCode.BadRequest, "Not Found");
        }

        StockAdjustment.AdjustmentAmount = update.AdjustmentAmount;
        StockAdjustment.AdjustmentDate = update.AdjustmentDate;
        StockAdjustment.Reason = update.Reason;
        StockAdjustment.ProductId = update.ProductId;
        

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetStockAdjustmentDto>(HttpStatusCode.BadRequest, "Not Updated")
            : mapper.Map<Response<GetStockAdjustmentDto>>(StockAdjustment);
    }
    
}