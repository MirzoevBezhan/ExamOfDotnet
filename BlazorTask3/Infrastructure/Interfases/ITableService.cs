using Domain.Dtos.Table;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfases;

public interface ITableService
{
    Task<PagedResponse<List<GetTableDto>>> GetAll(TableFilter filter);
    Task<Response<GetTableDto>> Create(CreateTableDto tableDto);
    Task<Response<GetTableDto>> Update(int id,UpdateTableDto tableDto);
    Task<Response<GetTableDto>> Get(int id);
    Task<Response<string>> Delete(int id);
}