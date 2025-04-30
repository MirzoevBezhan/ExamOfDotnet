namespace Domain.Entitites;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public virtual List<OrderItem> OrderItems { get; set; }
}