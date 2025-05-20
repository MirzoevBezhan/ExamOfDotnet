using Domain.DTOs.Analytics;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{

    [HttpPost("car-occupancy")]
    public async Task<IActionResult> GetCarOccupancy(AnalyticsDto request)
    {
        var response = await analyticsService.GetCarOccupancy(request);
        return Ok(response);
    }

    [HttpPost("customer-activity")]
    public async Task<IActionResult> GetCustomerActivity(AnalyticsDto request)
    {
        var response = await analyticsService.GetCustomerActivity(request);
        return Ok(response);
    }

    [HttpGet("top-models")]
    public async Task<IActionResult> GetTopModels(int month, int year)
    {
        var response = await analyticsService.GetTopModels(month, year);
        return Ok(response);
    }

    [HttpPost("total-revenue")]
    public async Task<IActionResult> GetTotalRevenue(AnalyticsDto request)
    {
        var response = await analyticsService.GetTotalRevenue(request);
        return Ok(response);
    }
}