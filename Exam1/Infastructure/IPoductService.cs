using Domain;

namespace Infastructure;

public interface IPoductService
{
    public List<Product> GetProductByCategory(string category);
    public List<Product> GetUniqueProducts();
    
}
