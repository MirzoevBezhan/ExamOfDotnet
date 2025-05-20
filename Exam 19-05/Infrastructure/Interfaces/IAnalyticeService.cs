using Domain.DTOs.Analytics;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IAnalyticsService
{
    Task<Response<decimal>> GetTotalRevenue(AnalyticsDto request);
    Task<Response<List<CarOccupancyDto>>> GetCarOccupancy(AnalyticsDto request);
    Task<Response<List<ActiveCustomersDto>>> GetCustomerActivity(AnalyticsDto request);
    Task<Response<List<PopularCarsDto>>> GetTopModels(int month, int year);
}