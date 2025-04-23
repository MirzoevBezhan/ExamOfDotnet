using Domain.DTOs.StockAdjustment;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IStockAdjustmentService
{
    Task<PagedResponse<List<GetStockAdjustmentDto>>> GetAll(StockAdjustmentFilter filter);
    Task<Response<GetStockAdjustmentDto>> Get(int id);
    Task<Response<GetStockAdjustmentDto>> Update(int id, UpdateStockAdjustmentDto update);
    Task<Response<GetStockAdjustmentDto>> Add(CreateStockAdjustmentDto create);
    Task<Response<string>> Delete(int id);
}
