using Domain.DTOs.Email;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class SendProductsCount(
    IServiceScopeFactory scopeFactory,
    ILogger<SendProductsCount> logger)
{
    public async Task ExecuteTask(CancellationToken stoppingToken)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var now = DateTimeOffset.UtcNow;

        var products = await context.Products.ToListAsync(stoppingToken);
        
        await context.SaveChangesAsync(stoppingToken);
        logger.LogInformation("Promotion statuses removed from {Count} products", products.Count);
    }
}