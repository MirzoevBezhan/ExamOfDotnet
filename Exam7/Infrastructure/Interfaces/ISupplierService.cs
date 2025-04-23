using Domain.DTOs.StockAdjustment;
using Domain.DTOs.Supplier;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ISupplierService
{
    Task<PagedResponse<List<GetSupplierDto>>> GetAll(SupplierFilter filter);
    Task<Response<GetSupplierDto>> Get(int id);
    Task<Response<GetSupplierDto>> Update(int id, UpdateSupplierDto update);
    Task<Response<GetSupplierDto>> Add(CreateSupplierDto create);
    Task<Response<string>> Delete(int id);
    Task<Response<List<GetSupplierWithProductsDto>>> GetSuppliersWithProducts();
}