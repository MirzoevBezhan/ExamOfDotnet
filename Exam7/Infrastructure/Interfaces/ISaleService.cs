using Domain.DTOs.Sale;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ISaleService
{
    Task<PagedResponse<List<GetSaleDto>>> GetAll(SaleFilter filter);
    Task<Response<GetSaleDto>> Get(int id);
    Task<Response<GetSaleDto>> Update(int id, UpdateSaleDto update);
    Task<Response<GetSaleDto>> Add(CreateSaleDto create);
    Task<Response<string>> Delete(int id);
    Task<PagedResponse<List<GetSalesByDateDto>>> GetSalesByDate(SalesFilter filter);
    Task<Response<List<GetSaleWithTopProductsDto>>> GetFiveSalesWithTopProducts();
    Task<Response<List<GetSalesDailyRevenueDto>>> GetSalesDailyRevenue();
    Task<Response<List<GetStatisticForDashboardDto>>> GetStatisticsForDashboard();

}
