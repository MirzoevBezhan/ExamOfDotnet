using Domain.DTOs.User.Product;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<Response<GetProductDto>> Get(int id);
    Task<PagedResponse<List<GetProductDto>>> GetAll(ProductFilter filter);
    Task<Response<GetProductDto>> Add(CreateProductDto input);
    Task<Response<GetProductDto>> Update(int id, UpdateProductDto input);
    Task<Response<string>> Delete(int id);
}