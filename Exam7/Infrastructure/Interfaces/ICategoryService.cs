using Domain.DTOs.Category;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<PagedResponse<List<GetCategoryDto>>> GetAll(CategoryFilter filter);
    Task<Response<GetCategoryDto>> Get(int id);
    Task<Response<GetCategoryDto>> Update(int id, UpdateCategoryDto update);
    Task<Response<GetCategoryDto>> Add(CreateCategoryDto create);
    Task<Response<string>> Delete(int id);
     Task<Response<GetCategoryWithProductsDto>> GetCategoriesWithProducts();

}
