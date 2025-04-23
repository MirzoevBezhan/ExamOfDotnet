using Domain.DTOs.Product;

namespace Domain.DTOs.Category;

public class GetCategoryWithProductsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProductsForCategoryDto> Products { get; set; }
}