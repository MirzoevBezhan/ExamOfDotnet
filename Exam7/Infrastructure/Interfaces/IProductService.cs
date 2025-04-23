using Domain.DTOs.Product;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<PagedResponse<List<GetProductDto>>> GetAll(ProductFilter filter);
    Task<Response<GetProductDto>> Get(int id);
    Task<Response<GetProductDto>> Update(int id, UpdateProductDto update);
    Task<Response<GetProductDto>> Add(CreateProductDto create);
    Task<Response<string>> Delete(int id);
    Task<Response<List<GetProductsWithLowStockDto>>> GetProductsWithLowStock();
    Task<Response<List<GetProductsStatisticDto>>> GetProductsWithStatistic();
}