using System.Net;
using AutoMapper;
using Domain.DTOs.StockAdjustment;
using Domain.DTOs.Supplier;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SupplierService(DataContext context, IMapper mapper) : ISupplierService
{
    public async Task<Response<GetSupplierDto>> Add(CreateSupplierDto create)
    {
        var Supplier = mapper.Map<Supplier>(create);

        await context.Suppliers.AddAsync(Supplier);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new Response<GetSupplierDto>(HttpStatusCode.BadRequest, "Don't Added")
            : mapper.Map<Response<GetSupplierDto>>(Supplier);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Supplier = await context.Suppliers.FindAsync(id);

        if (Supplier == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }

        context.Suppliers.Remove(Supplier);
        var res = await context.SaveChangesAsync();

        return new Response<string>("Deleted");
    }

    public async Task<Response<GetSupplierDto>> Get(int id)
    {
        var Supplier = await context.Suppliers.FindAsync(id);
        return Supplier == null
            ? new Response<GetSupplierDto>(HttpStatusCode.BadRequest, "Not Found")
            : mapper.Map<Response<GetSupplierDto>>(Supplier);
    }

    public async Task<PagedResponse<List<GetSupplierDto>>> GetAll(SupplierFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var Suppliers = context.Suppliers.AsQueryable();

        if (filter.Name != null)
        {
            Suppliers = Suppliers.Where(c => c.Name.Contains(filter.Name));
        }

        if (filter.Phone != null)
        {
            Suppliers = Suppliers.Where(c => c.Phone.Contains(filter.Phone));
        }

        var mapped = mapper.Map<List<GetSupplierDto>>(Suppliers);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageSize * (validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize);

        return mapper.Map<PagedResponse<List<GetSupplierDto>>>(data);
    }

    public async Task<Response<GetSupplierDto>> Update(int id, UpdateSupplierDto update)
    {
        var Supplier = await context.Suppliers.FindAsync(id);

        if (Supplier == null)
        {
            return new Response<GetSupplierDto>(HttpStatusCode.BadRequest, "Not Found");
        }

        Supplier.Name = update.Name;
        Supplier.Phone = update.Phone;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetSupplierDto>(HttpStatusCode.BadRequest, "Not Updated")
            : mapper.Map<Response<GetSupplierDto>>(Supplier);
    }

    // Task 9
    public async Task<Response<List<GetSupplierWithProductsDto>>> GetSuppliersWithProducts()
    {
        var Suppliers = await context.Suppliers
            .Select(s => new GetSupplierWithProductsDto
            {
                SupplierId = s.Id,
                SupplierName = s.Name,
                Products = s.Products.Select(p => p.Name).ToList()
            })
            .ToListAsync();

        return mapper.Map<Response<List<GetSupplierWithProductsDto>>>(Suppliers);
    }
    // Task 10
}
    