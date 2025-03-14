using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using Domain;
using Microsoft.VisualBasic;
using Npgsql;

namespace Infastructure;

public class ProductService : IPoductService
{
    private string connectionString = "Host=localhost,Username=postgres,Password=ipo90,DataBase=Exam_db";
    public List<Product> GetProductByCategory(string category)
    {
        List<Product> products = new List<Product>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = $"select * from products where category = {category}";
            var command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                var product = new Product();
                while (reader.Read())
                {
                    product.product_id = reader.GetInt32(0);
                    product.name = reader.GetString(1);
                    product.description = reader.GetString(2);
                    product.price = reader.GetDecimal(3);
                    product.category = reader.GetString(4);
                    product.stock_quantity = reader.GetInt32(5);
                    product.manufacter = reader.GetString(5);
                }
                products.Add(product);
            }
        }
        return products;
    }
    public List<Product> GetUniqueProducts()
    {   
        List<Product> products = new List<Product>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = $"select distinct manufacturer from products";
            var command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                var product = new Product();
                while (reader.Read())
                {
                    product.product_id = reader.GetInt32(0);
                    product.name = reader.GetString(1);
                    product.description = reader.GetString(2);
                    product.price = reader.GetDecimal(3);
                    product.category = reader.GetString(4);
                    product.stock_quantity = reader.GetInt32(5);
                    product.manufacter = reader.GetString(5);
                }
                products.Add(product);
            }
        }
        return products;
    }
   

}
