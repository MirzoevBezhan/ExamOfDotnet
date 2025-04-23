using System.Net;
using AutoMapper;
using Domain.DTOs.Sale;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SaleService(DataContext context, IMapper mapper) : ISaleService
{
    public async Task<PagedResponse<List<GetSaleDto>>> GetAll(SaleFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var Sales = context.Sales.AsQueryable();

        if (filter.From != null)
        {
            var now = DateTime.Now.Year;
            Sales = Sales.Where(c => now - c.StartDate.Year >= filter.From);
        }

        if (filter.To != null)
        {
            var now = DateTime.Now.Year;
            Sales = Sales.Where(c => now - c.StartDate.Year <= filter.To);
        }

        var mapped = mapper.Map<List<GetSaleDto>>(Sales);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageSize * (validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize);

        return mapper.Map<PagedResponse<List<GetSaleDto>>>(data);
    }

    public async Task<Response<GetSaleDto>> Add(CreateSaleDto create)
    {
        var Sale = mapper.Map<Sale>(create);

        await context.Sales.AddAsync(Sale);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new Response<GetSaleDto>(HttpStatusCode.BadRequest, "Don't Added")
            : mapper.Map<Response<GetSaleDto>>(Sale);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Sale = await context.Sales.FindAsync(id);

        if (Sale == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }

        context.Sales.Remove(Sale);
        var res = await context.SaveChangesAsync();

        return new Response<string>("Deleted");
    }

    public async Task<Response<GetSaleDto>> Get(int id)
    {
        var Sale = await context.Sales.FindAsync(id);
        return Sale == null
            ? new Response<GetSaleDto>(HttpStatusCode.BadRequest, "Not Found")
            : mapper.Map<Response<GetSaleDto>>(Sale);
    }

    //Task 4
    public async Task<PagedResponse<List<GetSalesByDateDto>>> GetSalesByDate(SalesFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var Sales = context.Sales.AsQueryable();

        if (filter.From != null)
        {
            Sales = Sales.Where(c => c.StartDate >= filter.From);
        }

        if (filter.To != null)
        {
            Sales = Sales.Where(c => c.StartDate <= filter.To);
        }

        var mapped = mapper.Map<List<GetSalesByDateDto>>(Sales);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageSize * (validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize);

        return mapper.Map<PagedResponse<List<GetSalesByDateDto>>>(data);
    }

    public async Task<Response<GetSaleDto>> Update(int id, UpdateSaleDto update)
    {
        var Sale = await context.Sales.FindAsync(id);

        if (Sale == null)
        {
            return new Response<GetSaleDto>(HttpStatusCode.BadRequest, "Not Found");
        }

        Sale.StartDate = update.StartDate;
        Sale.QuantitySold = update.QuantitySold;
        Sale.ProductId = update.ProductId;
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetSaleDto>(HttpStatusCode.BadRequest, "Not Updated")
            : mapper.Map<Response<GetSaleDto>>(Sale);
    }

// /Task 5
    public async Task<Response<List<GetSaleWithTopProductsDto>>> GetFiveSalesWithTopProducts()
    {
        var Sales = await context.Sales
            .GroupBy(s => s.ProductId)
            .Select(g => new GetSaleWithTopProductsDto()
            {
                Name = context.Products.Where(p => p.Id == g.Key).Select(p => p.Name).FirstOrDefault(),
                TotalSold = g.Sum(s => s.QuantitySold)
            })
            .OrderByDescending(p => p.TotalSold)
            .Take(5)
            .ToListAsync();

        return mapper.Map<Response<List<GetSaleWithTopProductsDto>>>(Sales);
    }

    // / Task 6
    public async Task<Response<List<GetSalesDailyRevenueDto>>> GetSalesDailyRevenue()
    {
        var Seven = DateTime.UtcNow.AddDays(-7);

        var revenueData = await context.Sales
            .Where(s => s.StartDate >= Seven)
            .GroupBy(s => s.StartDate.Date)
            .Select(g => new GetSalesDailyRevenueDto()
            {
                Revenue = g
                    .Sum(s => s.QuantitySold * context.Products
                        .Where(p => p.Id == s.ProductId)
                        .Select(p => p.Price)
                        .FirstOrDefault()),
                Date = g.Key
            })
            .OrderByDescending(r => r.Date)
            .ToListAsync();

        return mapper.Map<Response<List<GetSalesDailyRevenueDto>>>(revenueData);
    }

    // Tas K 10
    public async Task<Response<List<GetStatisticForDashboardDto>>> GetStatisticsForDashboard()
    {
        var AllStatistic = new
        {
            TotalProducts = await context.Products.CountAsync(),
            TotalRevenue = await context.Sales.SumAsync(s => s.QuantitySold * s.Product.Price),
            TotalSales = await context.Sales.SumAsync(s => s.QuantitySold)
        };

        return mapper.Map<Response<List<GetStatisticForDashboardDto>>>(AllStatistic);
    }
}