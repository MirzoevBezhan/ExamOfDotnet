using System.Text.Json;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class RedisCacheService(IDistributedCache cacheService , ILogger<RedisCacheService> logger)  : IRedisCacheService
{
        public async Task SetData<T>(string key, T data, int expireInMinutes)
    {
        var cacheOptions = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expireInMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(expireInMinutes)
        };

        var jsonSerializerOption = new JsonSerializerOptions { WriteIndented = true };
        
        var jsonData = JsonSerializer.Serialize(data, jsonSerializerOption);
        
        await cacheService.SetStringAsync(key, jsonData,cacheOptions);
    }

    public async Task<T?> GetData<T>(string key)
    {
        var data = await cacheService.GetStringAsync(key);
        return data != null
            ? JsonSerializer.Deserialize<T>(data)
            : default;
    }

    public async Task DeleteData(string key)
    {
        cacheService.Remove(key);
    }
    
}