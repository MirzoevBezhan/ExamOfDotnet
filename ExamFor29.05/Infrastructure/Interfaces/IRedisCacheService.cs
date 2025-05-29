using Domain.DTOs.User.Product;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IRedisCacheService
{
    Task SetData<T>(string key, T data, int expireInMinutes);
    Task<T> GetData<T>(string key);
    Task DeleteData(string key);
}