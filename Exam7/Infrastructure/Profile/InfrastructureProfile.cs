
using Domain.DTOs.Category;
using Domain.DTOs.Product;
using Domain.DTOs.Sale;
using Domain.DTOs.StockAdjustment;
using Domain.DTOs.Supplier;
using Domain.Entities;

namespace Infrastructure.Profile;

public class InfrastructureProfile : AutoMapper.Profile
{
    public InfrastructureProfile()
    {
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<CreateSaleDto, Sale>();
        CreateMap<CreateStockAdjustmentDto, StockAdjustment>();
        CreateMap<CreateSupplierDto, Supplier>();

        CreateMap<Category,GetCategoryDto>();
        CreateMap<Product,GetProductDto>();
        CreateMap<Sale,GetSaleDto>();
        CreateMap<StockAdjustment,GetStockAdjustmentDto>();
        CreateMap<Supplier,GetSupplierDto>();
        CreateMap<Product,GetProductsWithLowStockDto>();
        CreateMap<Product,GetProductsStatisticDto>();
    }
}