using System.Net;
using AutoMapper;
using Domain.Dtos.Table;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfases;

namespace Infrastructure.Services;

public class TableService(DataContext context, IMapper mapper) : ITableService
{
    public async Task<PagedResponse<List<GetTableDto>>> GetAll(TableFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNumber, filter.PageSize);

        var Tables = context.Tables.AsQueryable();

        if (filter.Number != null)
        {
            Tables = Tables.Where(t => t.Number == filter.Number);
        }

        if (filter.Seats != null)
        {
            Tables = Tables.Where(t => t.Seats == filter.Seats);
        }

        var mapped = mapper.Map<List<GetTableDto>>(Tables);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageNumber * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();


        return new PagedResponse<List<GetTableDto>>(data, validFilter.PageNumber, validFilter.PageSize,
            totalRecords);
    }

    public async Task<Response<GetTableDto>> Create(CreateTableDto tableDto)
    {
        var Table = mapper.Map<Table>(tableDto);

        await context.Tables.AddAsync(Table);

        var res = await context.SaveChangesAsync();

        if (res == 0)
        {
            return new Response<GetTableDto>(HttpStatusCode.BadRequest, "Not Added");
        }

        return mapper.Map<Response<GetTableDto>>(Table);
    }

    public async Task<Response<GetTableDto>> Update(int id, UpdateTableDto tableDto)
    {
        var Table = await context.Tables.FindAsync(id);
        if (Table == null)
        {
            return new Response<GetTableDto>(HttpStatusCode.NotFound, "Not Found");
        }

        Table.Seats = tableDto.Seats;
        Table.Number = tableDto.Number;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetTableDto>(HttpStatusCode.BadRequest, "Not Updated")
            : mapper.Map<Response<GetTableDto>>(Table);
    }

    public async Task<Response<GetTableDto>> Get(int id)
    {
        var Table = await context.Tables.FindAsync(id);
        if (Table == null)
        {
            return new Response<GetTableDto>(HttpStatusCode.NotFound, "Not Found");
        }

        return mapper.Map<Response<GetTableDto>>(Table);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Table = await context.Tables.FindAsync(id);

        if (Table == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not Found");
        }

        context.Tables.Remove(Table);
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Not Deleted")
            : new Response<string>(HttpStatusCode.OK, "Deleted");
    }
}