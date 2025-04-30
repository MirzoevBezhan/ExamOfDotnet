using Domain.Dtos.Customer;
using Domain.Dtos.Product;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<PagedResponse<List<GetProductDto>>> GetAll(ProductFilter filter);
    Task<Response<GetProductDto>> GetById(int id);
    Task<Response<GetProductDto>> Update(int id,UpdateProductDto updateProductDto);
    Task<Response<GetProductDto>> Create(CreateProductDto createProductDto);
    Task<Response<string>> Delete(int id);
}