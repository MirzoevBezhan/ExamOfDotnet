using Domain;
using Infastructure;

ProductService productService = new ProductService();
foreach (var item in productService.GetUniqueProducts())
{
 System.Console.WriteLine(item.name);    
 System.Console.WriteLine(item.category);    
 System.Console.WriteLine(item.price);    
}